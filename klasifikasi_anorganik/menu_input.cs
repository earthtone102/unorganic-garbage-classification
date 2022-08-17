using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace klasifikasi_anorganik
{
    public partial class menu_input : Form
    {
        public struct dataGambar
        {
            public string kategori;
            //glcm
            public double asm;
            public double kontras;
            public double idm;
            public double korelasi;

            //glrlm
            public double sre;
            public double lre;
            public double glu;
            public double rlu;
            public double rp;
        }
        public Bitmap bmp, pictGrayScale, pictConstrastS, pictOtsu;
        public string namaFile, namaFolder,material;
        

        public double[] valueGlcm = new double[4]; //ciri Glcm
        public double[] valueGlrlm = new double[5]; //ciri Glrlm
        Color clr, clr2;
        bool cekUji = false;
        int[] dot = new int[4];

        public menu_input()
        {
            InitializeComponent();
        }
        public Bitmap gray_scale(Bitmap img)
        {
            Bitmap grayS = (Bitmap)img.Clone();
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Color clr = img.GetPixel(j, i);
                    int warna = (clr.R + clr.G + clr.B) / 3;
                    grayS.SetPixel(j, i, Color.FromArgb(warna, warna, warna));
                }
            }
            return grayS;
        }
        public Bitmap contrast_stretching(Bitmap img)
        {
            Bitmap cs = (Bitmap)img.Clone();
            int rmin = 60;
            int rmax = 100;

            for(int i=0;i<img.Height;i++)
            {
                for(int j=0;j<img.Width;j++)
                {
                    Color clr = cs.GetPixel(j, i);
                    int warna = clr.R;

                    if (warna < rmin)
                        warna = 0;
                    else if (warna > rmin && warna < rmax)
                        warna = ((warna - rmax) / (rmin - rmax)) * 255;
                    cs.SetPixel(j,i, Color.FromArgb(warna, warna, warna));
                }
            }
            return cs;
        }
        public int[] dot_segmentasi (Bitmap img)
        {
            Bitmap otsu = (Bitmap)img.Clone();

            int t = 180, tnew = 170;
            int[] lebih_besar = new int[256];
            int[] lebih_kecil = new int[256];
            int mean_besar = 0, mean_kecil = 0;
            int count_besar = 0, count_kecil = 0;

            //inisialisasi array
            for(int i=0;i<256;i++)
            {
                lebih_besar[i] = 0;
                lebih_kecil[i] = 0;
            }

            int delta = t - tnew;
            while (delta !=0)
            {
                for(int i=0;i<otsu.Height;i++)
                {
                    for(int j=0;j<otsu.Width;j++)
                    {
                        clr = otsu.GetPixel(j, i);
                        int warna = clr.R;
                        if (warna > t)
                            lebih_besar[warna] = 1;
                        else if (warna <= t)
                            lebih_kecil[warna] = 1;
                    }
                }
                for(int i=0;i<256;i++)
                {
                    if(lebih_besar[i]>0)
                    {
                        mean_besar += i;
                        count_besar += 1;
                    }
                    else if(lebih_kecil[i]>0)
                    {
                        mean_kecil += i;
                        count_kecil += 1;
                    }
                }

                mean_besar /= count_besar;
                mean_kecil /= count_kecil;
                tnew = (mean_besar + mean_kecil) / 2;

                delta = t - tnew;
                if (delta < 0)
                    delta = delta * (-1);

                t = tnew;

                //set segmentasi ke gambar
                for(int i=0;i<otsu.Height;i++)
                {
                    for(int j=0;j<otsu.Width;j++)
                    {
                        clr = otsu.GetPixel(j, i);
                        int warna = clr.R;
                        if (warna > t)
                            warna = 255;
                        else
                            warna = 0;
                        otsu.SetPixel(j, i, Color.FromArgb(warna, warna, warna));
                    }
                }
            }//akhir while

            int[] dot = new int[4];
            int min_height = 1000000, min_width = 1000000, max_height = 0, max_width = 0;
            for (int i = 0; i < otsu.Height; i++)
            {
                for (int j = 0; j < otsu.Width; j++)
                {
                    clr = otsu.GetPixel(j, i);
                    int warna = clr.R;
                    if (warna != 255) //antara 0 sampai pertengahan
                    {
                        if (i < min_height)
                            min_height = i;
                        else if (i > max_height)
                            max_height = i;

                        if (j < min_width)
                            min_width = j;
                        else if (j > max_width)
                            max_width = j;
                    }
                }
            }
            dot[0] = min_height;
            dot[1] = min_width;
            dot[2] = max_height;
            dot[3] = max_width;

            return dot;
        }
        public int [,] initMat(int height,int width)
        {
            int[,] mat = new int[height, width];
            for(int i=0;i<height;i++)
            {
                for (int j = 0; j < width; j++)
                    mat[i, j] = 0;
            }

            return mat;
        }
        public double [] fungsi_glcm (Bitmap img,int sudut, int[]dot)
        {
            int[,] matTranspos; //transpos matriks glcm
            double[] ciriGlcm = new double[4]; //array hasil ekstasi glcm
            int minHeight = dot[0];
            int minWidth = dot[1];
            int maxHeight = dot[2];
            int maxWidth = dot[3];

            double[,] matNormalisasi = new double[256, 256]; //matriks untuk hasil normalisasi mat glcm
            for(int i=0;i<256;i++)
            {
                for (int j = 0; j < 256; j++)
                    matNormalisasi[i, j] = 0;
            }

            int[,] matGLCM = new int[256,256]; //matriks glcm
            //inisialisasi matriks glcm
            matGLCM = initMat(256, 256);

            //glcm
            for (int i=minHeight;i<=maxHeight;i++)
            {
                for(int j=minWidth;j<=maxWidth;j++)
                {
                    //glcm sudut 0
                    if (sudut == 0)
                    {
                        if (j == maxWidth)
                            continue;
                        clr = img.GetPixel(j, i);
                        clr2 = img.GetPixel(j + 1, i);
                        matGLCM[clr.R, clr2.R] += 1;
                    }
                    else if (sudut == 45)
                    {
                        if (i == minHeight || j == maxWidth)
                            continue;
                        clr = img.GetPixel(j, i);
                        clr2 = img.GetPixel(j + 1, i - 1);
                        matGLCM[clr.R, clr2.R] += 1;
                    }
                    else if(sudut == 90)
                    {
                        if (i == minHeight)
                            continue;
                        clr = img.GetPixel(j, i);
                        clr2 = img.GetPixel(j, i - 1);
                        matGLCM[clr.R, clr2.R] += 1;
                    }
                    else if(sudut ==135)
                    {
                        if (j == minWidth || i == minHeight)
                            continue;
                        clr = img.GetPixel(j, i);
                        clr2 = img.GetPixel(j - 1, i - 1);
                        matGLCM[clr.R, clr2.R] += 1;
                    }
                }
            }

            
            //inisialisasi matrix transpos
            matTranspos = initMat(256, 256);

            //transpos matrix
            for (int i=0;i<256;i++)
            {
                for (int j = 0; j < 256; j++)
                    matTranspos[i, j] = matGLCM[j, i];
            }

            double jumlah = 0;

            //tambahkan matriks hasil dengan matriks transpos
            for(int i=0;i<256;i++)
            {
                for(int j=0;j<256;j++)
                {
                    matGLCM[i, j] += matTranspos[i, j];
                    jumlah += matGLCM[i, j];
                }
            }

            //normalisasi, bagikan setiap pixel dengan nilai dari seluruh pixel
            for(int i=0;i<256;i++)
            {
                for(int j=0;j<256;j++)
                    matNormalisasi[i, j] = matGLCM[i,j] / jumlah;
            }

            //ekstrasi ciri
            double glcmAsm = 0;
            double glcmKontras = 0;
            double glcmIdm = 0;
            double glcmKorelasi = 0;
            double glcmMuI = 0, glcmMuJ = 0, glcmSigmaI = 0, glcmSigmaJ = 0;

            //korelasi, mu untuk i, dan mu untuk j
            for(int i=0;i<256;i++)
            {
                for(int j=0;j<256;j++)
                {
                    if(matNormalisasi[i,j]!=0)
                    {
                        glcmMuI += i * matNormalisasi[i, j];
                        glcmMuJ += j * matNormalisasi[i, j];
                    }
                }
            }

            //korelasi, sigma untu i, dan sigma untuk j
            for(int i=0;i<256;i++)
            {
                for(int j=0;j<256;j++)
                {
                    if (matNormalisasi[i, j] != 0)
                    {
                        glcmSigmaI += matNormalisasi[i, j] * Math.Pow(i - glcmMuI, 2);
                        glcmSigmaJ += matNormalisasi[i, j] * Math.Pow(j - glcmMuJ, 2);
                    }
                }
            }
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if (matNormalisasi[i, j] != 0)
                    {
                        //Angular second moment(ASM)
                        glcmAsm += Math.Pow(matNormalisasi[i, j], 2);
                        //kontras
                        glcmKontras += Math.Pow(i - j, 2) * matNormalisasi[i, j];
                        //idm
                        glcmIdm += matNormalisasi[i, j] / (1 + Math.Pow(i - j, 2));
                        //korelasi
                        glcmKorelasi += ((i - glcmMuI) * (j - glcmMuJ) * matNormalisasi[i, j]) / (Math.Sqrt(glcmSigmaI) * Math.Sqrt(glcmSigmaJ));
                    }
                }
            }

            //masukkan nilai ekstrasi kedalam array
            ciriGlcm[0] = glcmAsm;
            ciriGlcm[1] = glcmKontras;
            ciriGlcm[2] = glcmIdm;
            ciriGlcm[3] = glcmKorelasi;
            return ciriGlcm;
        }

        public double[] fungsi_glrlm(Bitmap img, int sudut, int[]dot)
        {
            //minHeight adalah mulai nilai terkecil baris, minWidth adalah mulai nilai terkecil kolom
            //maxHeight adalah mulai nilai terbesar baris, maxWidth adalah mulai nilai terbesar kolom

            int minHeight = dot[0];
            int minWidth = dot[1];
            int maxHeight = dot[2];
            int maxWidth = dot[3];

            int height = maxHeight - minHeight + 1;
            int width = maxWidth - minWidth + 1;
            int baris, kolom;

            int[,] matGLRLM;
            matGLRLM = initMat(256, width+1);//inisialisasi matriks glrlm
            double[] ciriGlrlm = new double[5]; //untuk tampung nilai hasil ekstrasi
            List<int> pixel = new List<int>();
            
            

            int hitung = 1;
            //pembentukan matriks glrlm
            if (sudut == 0)
            {
                for(int i=minHeight;i<=maxHeight;i++)
                {
                    for(int j=minWidth;j<=maxWidth;j++)
                    {
                        clr = img.GetPixel(j, i);
                        pixel.Add(clr.R);
                    }
                    hitung = 1;
                    if (pixel.Count() == 1)
                        matGLRLM[pixel[0], hitung] += 1;
                    else
                    {
                        for (int k = 0; k < pixel.Count(); k++)
                        {
                            if (k == pixel.Count() - 1 && hitung == 1)
                                matGLRLM[pixel[k], hitung] += 1;
                            else if (k == pixel.Count() - 1 && hitung != 1)
                            {
                                matGLRLM[pixel[k], hitung] += 1;
                                hitung = 1;
                            }
                            else
                            {
                                if (pixel[k] != pixel[k + 1] && hitung == 1)
                                    matGLRLM[pixel[k], hitung] += 1;
                                else if (pixel[k] != pixel[k + 1] && hitung != 1)
                                {
                                    matGLRLM[pixel[k], hitung] += 1;
                                    hitung = 1;
                                }
                                else if (pixel[k] == pixel[k + 1])
                                    hitung += 1;
                            }
                        }
                    }
                    pixel.Clear();
                }
            }//akhir sudut 0
            else if (sudut == 45)
            {
                for(int i=minHeight;i<=maxHeight;i++)
                {
                    baris = i;
                    for(int j=maxWidth;j>=minWidth;j--)
                    {
                        if (baris <= maxHeight)
                        {
                            clr = img.GetPixel(j, baris);
                            pixel.Add(clr.R);
                        }
                        else if (baris > maxHeight)
                            break;
                        baris += 1;
                    }
                    hitung = 1;
                    if (pixel.Count() == 1)
                        matGLRLM[pixel[0], hitung] += 1;
                    else
                    {
                        for (int k = 0; k < pixel.Count(); k++)
                        {
                            if (k == pixel.Count() - 1 && hitung == 1)
                                matGLRLM[pixel[k], hitung] += 1;
                            else if (k == pixel.Count() - 1 && hitung != 1)
                            {
                                matGLRLM[pixel[k], hitung] += 1;
                                hitung = 1;
                            }
                            else
                            {
                                if (pixel[k] != pixel[k + 1] && hitung == 1)
                                    matGLRLM[pixel[k], hitung] += 1;
                                else if (pixel[k] != pixel[k + 1] && hitung != 1)
                                {
                                    matGLRLM[pixel[k], hitung] += 1;
                                    hitung = 1;
                                }
                                else if (pixel[k] == pixel[k + 1])
                                    hitung += 1;
                            }
                        }
                    }
                    pixel.Clear();
                }
                for(int j=maxWidth-1;j>=minWidth;j--)
                {
                    kolom = j;
                    for(int i=minHeight;i<=maxHeight;i++)
                    {
                        if (kolom >= minWidth)
                        {
                            clr = img.GetPixel(kolom, i);
                            pixel.Add(clr.R);
                        }
                        else if (kolom < minWidth)
                            break;
                        kolom -= 1;
                    }
                    hitung = 1;
                    if (pixel.Count() == 1)
                        matGLRLM[pixel[0], hitung] += 1;
                    else
                    {
                        for (int k = 0; k < pixel.Count(); k++)
                        {
                            if (k == pixel.Count() - 1 && hitung == 1)
                                matGLRLM[pixel[k], hitung] += 1;
                            else if (k == pixel.Count() - 1 && hitung != 1)
                            {
                                matGLRLM[pixel[k], hitung] += 1;
                                hitung = 1;
                            }
                            else
                            {
                                if (pixel[k] != pixel[k + 1] && hitung == 1)
                                    matGLRLM[pixel[k], hitung] += 1;
                                else if (pixel[k] != pixel[k + 1] && hitung != 1)
                                {
                                    matGLRLM[pixel[k], hitung] += 1;
                                    hitung = 1;
                                }
                                else if (pixel[k] == pixel[k + 1])
                                    hitung += 1;
                            }
                        }
                    }
                    pixel.Clear();
                }
            }//akhir sudut 45
            else if (sudut == 90)
            {
                matGLRLM = initMat(256, height + 1);//inisialisasi matriks glrlm
                for (int j=minWidth;j<=maxWidth;j++)
                {
                    for(int i=maxHeight;i>=minHeight;i--)
                    {
                        clr = img.GetPixel(j, i);
                        pixel.Add(clr.R);
                    }
                    hitung = 1;
                    if (pixel.Count() == 1)
                        matGLRLM[pixel[0], hitung] += 1;
                    else
                    {
                        for (int k = 0; k < pixel.Count(); k++)
                        {
                            if (k == pixel.Count() - 1 && hitung == 1)
                                matGLRLM[pixel[k], hitung] += 1;
                            else if (k == pixel.Count() - 1 && hitung != 1)
                            {
                                matGLRLM[pixel[k], 1] += 1;
                                hitung = 1;
                            }
                            else
                            {
                                if (pixel[k] != pixel[k + 1] && hitung == 1)
                                    matGLRLM[pixel[k], hitung] += 1;
                                else if (pixel[k] != pixel[k + 1] && hitung != 1)
                                {
                                    matGLRLM[pixel[k], hitung] += 1;
                                    hitung = 1;
                                }
                                else if (pixel[k] == pixel[k + 1])
                                    hitung += 1;
                            }
                        }
                    }
                    pixel.Clear();
                }
            }//akhir sudut 90
            else if (sudut == 135)
            {
                for(int i=minHeight;i<=maxHeight;i++)
                {
                    baris = i;
                    for(int j=minWidth;j<=maxWidth;j++)
                    {
                        if (baris <= maxHeight)
                        {
                            clr = img.GetPixel(j, baris);
                            pixel.Add(clr.R);
                        }
                        else if (baris > maxHeight)
                            break;
                        baris += 1;
                    }
                    hitung = 1;
                    if (pixel.Count() == 1)
                        matGLRLM[pixel[0], hitung] += 1;
                    else
                    {
                        for (int k = 0; k < pixel.Count(); k++)
                        {
                            if (k == pixel.Count() - 1 && hitung == 1)
                                matGLRLM[pixel[k], hitung] += 1;
                            else if (k == pixel.Count() - 1 && hitung != 1)
                            {
                                matGLRLM[pixel[k], hitung] += 1;
                                hitung = 1;
                            }
                            else
                            {
                                if (pixel[k] != pixel[k + 1] && hitung == 1)
                                    matGLRLM[pixel[k], hitung] += 1;
                                else if (pixel[k] != pixel[k + 1] && hitung != 1)
                                {
                                    matGLRLM[pixel[k], hitung] += 1;
                                    hitung = 1;
                                }
                                else if (pixel[k] == pixel[k + 1])
                                    hitung += 1;
                            }
                        }
                    }
                    pixel.Clear();
                }
                for(int j=minWidth;j<=maxWidth;j++)
                {
                    kolom = j + 1;
                    for(int i=minHeight;i<=maxHeight;i++)
                    {
                        if (kolom <= maxWidth)
                        {
                            clr = img.GetPixel(kolom, i);
                            pixel.Add(clr.R);
                        }
                        else if (kolom > maxWidth)
                            break;
                        kolom += 1;
                    }
                    hitung = 1;
                    if (pixel.Count() == 1)
                        matGLRLM[pixel[0], hitung] += 1;
                    else
                    {
                        for (int k = 0; k < pixel.Count(); k++)
                        {
                            if (k == pixel.Count() - 1 && hitung == 1)
                                matGLRLM[pixel[k], hitung] += 1;
                            else if (k == pixel.Count() - 1 && hitung != 1)
                            {
                                matGLRLM[pixel[k], hitung] += 1;
                                hitung = 1;
                            }
                            else
                            {
                                if (pixel[k] != pixel[k + 1] && hitung == 1)
                                    matGLRLM[pixel[k], hitung] += 1;
                                else if (pixel[k] != pixel[k + 1] && hitung != 1)
                                {
                                    matGLRLM[pixel[k], hitung] += 1;
                                    hitung = 1;
                                }
                                else if (pixel[k] == pixel[k + 1])
                                    hitung += 1;
                            }
                        }
                    }
                    pixel.Clear();
                }
            }//akhir sudut 135
            double s = 0;
            int panjang = 0;

            if (sudut == 90)
                panjang = height + 1;
            else
                panjang = width + 1;

            //hitung total nilai run length
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < panjang; j++)
                    s += matGLRLM[i, j];
            }
            //ekstraksi ciri
            double glrlmSre = 0, glrlmLre = 0, glrlmGlu = 0, glrlmRlu = 0, glrlmRp = 0;
            for(int i=0;i<256;i++)
            {
                for(int j=0;j< panjang; j++)
                {
                    if(matGLRLM[i,j]!=0)
                    {
                        glrlmSre += (matGLRLM[i, j] /s) / Math.Pow(j, 2);
                        glrlmLre += Math.Pow(j, 2) * matGLRLM[i, j] / s;
                        glrlmGlu += Math.Pow(matGLRLM[i, j], 2) / s;
                        glrlmRp += matGLRLM[i, j] / (double)(width * height);
                    }

                }
            }
            for(int i=0;i<panjang; i++)
            {
                for(int j=0;j<256;j++)
                {
                    if (matGLRLM[j, i] != 0)
                        glrlmRlu += Math.Pow(matGLRLM[j, i], 2) / s;
                }
            }
            ciriGlrlm[0] = glrlmSre;
            ciriGlrlm[1] = glrlmLre;
            ciriGlrlm[2] = glrlmGlu;
            ciriGlrlm[3] = glrlmRlu;
            ciriGlrlm[4] = glrlmRp;
            return ciriGlrlm;
        }
        public void sort(List<double>value, short[] index)
        {
            double temp = 0;
            short tempIndex = 0;
            for (int i = 0; i < value.Count(); i++)
            {
                for (int j = i + 1; j < value.Count(); j++)
                {
                    if (value[i] > value[j])
                    {
                        temp = value[j];
                        value[j] = value[i];
                        value[i] = temp;

                        tempIndex = index[j];
                        index[j] = index[i];
                        index[i] = tempIndex;
                    }
                }
            }
        }

        public void tulis_file(string namaFolder, int kode, int sudut)
        {
            Bitmap img,cs,gs;
            dataGambar data;

            string anImage = "", temp = "", namaImage;
            
            int[] dot = new int[4];
            double[] glcmDataset = new double[4];
            double[] glrlmDataset = new double[5];

            string namaFile = "";
            if (kode == 1)
                namaFile = "ciriGlcm"+sudut.ToString()+".txt";
            else if (kode == 2)
                namaFile = "ciriGlrlm" + sudut.ToString() + ".txt";
            StreamWriter tulis = new StreamWriter(namaFile);
            var files = Directory.GetFiles(namaFolder);

            for (int i = 0; i < files.Count(); i++)
            {
                anImage = files[i];
                img = Bitmap.FromFile(anImage) as Bitmap; //gambar

                temp = Path.GetFileNameWithoutExtension(anImage);
                namaImage = "";
                for (int j = 0; j < temp.Length; j++)
                {
                    if (char.IsDigit(temp[j]) == true)
                        continue;
                    namaImage += temp[j];
                }
                //pre proses dataset
                gs = gray_scale(img);
                cs = contrast_stretching(gs);
                dot = dot_segmentasi(cs);

                data.kategori = namaImage; //kategori

                //ekstrasi ciri citra latih
                if (kode == 1) //glcm
                {
                    glcmDataset = fungsi_glcm(cs, sudut, dot);
                    data.asm = glcmDataset[0];
                    data.kontras = glcmDataset[1];
                    data.idm = glcmDataset[2];
                    data.korelasi = glcmDataset[3];
                    tulis.WriteLine(data.kategori + "|" + data.asm + "|" + data.kontras + "|" + data.idm + "|" + data.korelasi);
                }
                else if (kode == 2)//glrlm 
                {
                    glrlmDataset = fungsi_glrlm(cs, sudut, dot);
                    data.sre = glrlmDataset[0];
                    data.lre = glrlmDataset[1];
                    data.glu = glrlmDataset[2];
                    data.rlu = glrlmDataset[3];
                    data.rp = glrlmDataset[4];
                    tulis.WriteLine(data.kategori + "|" + data.sre + "|" + data.lre + "|" + data.glu + "|" + data.rlu + "|" + data.rp);
                }
            }
            tulis.Close();
        }
        public string klasifikasi(int kode, int sudut)
        {
            int jumlah = 300;

            string fileGlcm = "", fileGlrlm = "";
            bool cekGlcm = false, cekGlrlm = false, cekGabung=false;
            string hasil = "";

            string[] kategori = new string[jumlah]; //untuk tampung kategori
            string[] kata;

            double asm = 0, kontras = 0, idm = 0, korelasi = 0;
            double sre = 0, lre = 0, glu = 0, rlu = 0, rp = 0;

            int index = 0;

            double distance = 0;
            List<double> distanceAllNeighbors = new List<double>();
            distanceAllNeighbors.Clear();
            short[] indexAllNeighbors = new short[jumlah];
            

            if(kode ==1)
            {
                fileGlcm = "ciriGlcm" + sudut.ToString() + ".txt";
                if (File.Exists(fileGlcm) == true)
                    cekGlcm = true;
                else
                {
                    tulis_file(namaFolder, 1, sudut);
                    cekGlcm = true;
                }
                if(cekGlcm == true)
                {
                    foreach(string line in File.ReadLines(fileGlcm))//baca perbaris
                    {
                        kata = line.Split(new char[] { '|' });
                        for (int i = 0; i < kata.Count(); i++)
                            kata[i].Replace('.', ',');

                        kategori[index] = kata[0];
                        asm = double.Parse(kata[1]);
                        kontras = double.Parse(kata[2]);
                        idm = double.Parse(kata[3]);
                        korelasi = double.Parse(kata[4]);

                        distance = Math.Sqrt(Math.Pow(valueGlcm[0] - asm, 2) + Math.Pow(valueGlcm[1] - kontras, 2) + Math.Pow(valueGlcm[2] - idm, 2) + Math.Pow(valueGlcm[3] - korelasi, 2));
                        distanceAllNeighbors.Add(distance);
                        indexAllNeighbors[index] = (short)index;

                        index += 1;
                    }
                }
            }
            else if (kode==2)
            {
                fileGlrlm = "ciriGlrlm" + sudut.ToString() + ".txt";
                if (File.Exists(fileGlrlm) == true)
                    cekGlrlm = true;
                else
                {
                    tulis_file(namaFolder, 2, sudut);
                    cekGlrlm = true;
                }
                if(cekGlrlm==true)
                {
                    foreach(string line in File.ReadLines(fileGlrlm))
                    {
                        kata = line.Split(new char[] { '|' });
                        for (int i = 0; i < kata.Count(); i++)
                            kata[i].Replace('.', ',');
                        kategori[index] = kata[0];
                        sre = double.Parse(kata[1]);
                        lre = double.Parse(kata[2]);
                        glu = double.Parse(kata[3]);
                        rlu = double.Parse(kata[4]);
                        rp = double.Parse(kata[5]);

                        distance = Math.Sqrt(Math.Pow(valueGlrlm[0] - sre, 2) + Math.Pow(valueGlrlm[1] - lre, 2) + Math.Pow(valueGlrlm[2] - glu, 2) + Math.Pow(valueGlrlm[3] - rlu, 2) + Math.Pow(valueGlrlm[4] - rp, 2));
                        distanceAllNeighbors.Add(distance);
                        indexAllNeighbors[index] = (short)index;
                        index += 1;
                    }
                }
            }
            else if(kode ==3)
            {
                kategori = new string[jumlah * 2];
                indexAllNeighbors = new short[jumlah*2];
                fileGlcm = "ciriGlcm" + sudut.ToString() + ".txt";
                fileGlrlm = "ciriGlrlm" + sudut.ToString() + ".txt";
                if (File.Exists(fileGlcm) == true && File.Exists(fileGlrlm) == true)
                    cekGabung = true;
                else if(File.Exists(fileGlcm)==false &&File.Exists(fileGlrlm)==true)
                {
                    tulis_file(namaFolder, 1, sudut);
                    cekGabung = true;
                }
                else if (File.Exists(fileGlcm) == true && File.Exists(fileGlrlm) == false)
                {
                    tulis_file(namaFolder, 2, sudut);
                    cekGabung = true;
                }
                else if (File.Exists(fileGlcm) == false && File.Exists(fileGlrlm) == false)
                {
                    tulis_file(namaFolder, 1, sudut);
                    tulis_file(namaFolder, 2, sudut);
                    cekGabung = true;
                }
                if(cekGabung == true)
                {
                    //glcm
                    foreach (string line in File.ReadLines(fileGlcm))//baca perbaris
                    {
                        kata = line.Split(new char[] { '|' });
                        for (int i = 0; i < kata.Count(); i++)
                            kata[i].Replace('.', ',');

                        kategori[index] = kata[0];
                        asm = double.Parse(kata[1]);
                        kontras = double.Parse(kata[2]);
                        idm = double.Parse(kata[3]);
                        korelasi = double.Parse(kata[4]);

                        distance = Math.Sqrt(Math.Pow(valueGlcm[0] - asm, 2) + Math.Pow(valueGlcm[1] - kontras, 2) + Math.Pow(valueGlcm[2] - idm, 2) + Math.Pow(valueGlcm[3] - korelasi, 2));
                        distanceAllNeighbors.Add(distance);
                        indexAllNeighbors[index] = (short)index;

                        index += 1;
                    }
                    //glrlm
                    foreach (string line in File.ReadLines(fileGlrlm))
                    {
                        kata = line.Split(new char[] { '|' });
                        for (int i = 0; i < kata.Count(); i++)
                            kata[i].Replace('.', ',');
                        kategori[index] = kata[0];
                        sre = double.Parse(kata[1]);
                        lre = double.Parse(kata[2]);
                        glu = double.Parse(kata[3]);
                        rlu = double.Parse(kata[4]);
                        rp = double.Parse(kata[5]);

                        distance = Math.Sqrt(Math.Pow(valueGlrlm[0] - sre, 2) + Math.Pow(valueGlrlm[1] - lre, 2) + Math.Pow(valueGlrlm[2] - glu, 2) + Math.Pow(valueGlrlm[3] - rlu, 2) + Math.Pow(valueGlrlm[4] - rp, 2));
                        distanceAllNeighbors.Add(distance);
                        indexAllNeighbors[index] = (short)index;
                        index += 1;
                    }
                }
            }
            sort(distanceAllNeighbors, indexAllNeighbors);
            //dapatkan 3 nilai distance terkecil
            double nndistance1 = distanceAllNeighbors[0];
            double nndistance2 = distanceAllNeighbors[1];
            double nndistance3 = distanceAllNeighbors[2];

            //dapatkan kategorinya
            string nnkategori1 = kategori[indexAllNeighbors[0]];
            string nnkategori2 = kategori[indexAllNeighbors[1]];
            string nnkategori3 = kategori[indexAllNeighbors[2]];

            //hitung bobot masing masing kategori

            int fungsiy1, fungsiy2, fungsiy3;
            double y1 = 0, y2 = 0, y3 = 0;

            // kategori 1
            for (int i=0;i< jumlah;i++)
            {
                if (i == indexAllNeighbors[0])
                    continue;
                else
                {
                    if (kategori[i] == kategori[indexAllNeighbors[0]])
                        fungsiy1 = 1;
                    else
                        fungsiy1 = 0;
                }
                y1 += (nndistance1 * fungsiy1);
            }
            // kategori 2
            for (int i = 0; i < jumlah; i++)
            {
                if (i == indexAllNeighbors[1])
                    continue;
                else
                {
                    if (kategori[i] == kategori[indexAllNeighbors[1]])
                        fungsiy2 = 1;
                    else
                        fungsiy2 = 0;
                }
                y2 += (nndistance2 * fungsiy2);
            }
            // kategori 3
            for (int i = 0; i < jumlah; i++)
            {
                if (i == indexAllNeighbors[2])
                    continue;
                else
                {
                    if (kategori[i] == kategori[indexAllNeighbors[2]])
                        fungsiy3 = 1;
                    else
                        fungsiy3 = 0;
                }
                y3 += (nndistance3 * fungsiy3);
            }
            double[] y = { y1, y2, y3 };
            double max = -1;
            for(int i=0;i<y.Count();i++)
            {
                if (y[i] > max)
                    max = y[i];
            }
            if (max == y1)
                hasil = nnkategori1;
            else if (max == y2)
                hasil = nnkategori2;
            else
                hasil = nnkategori3;
            
            return hasil;
        }
        
        private void btnPilihGambar_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG | *.jpg";
            //openFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openFileDialog1.InitialDirectory = !Directory.Exists(namaFolder) ? Path.GetPathRoot(Environment.SystemDirectory) : @"D:\skripsi\citra uji\";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pb1.Image = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                namaFile = openFileDialog1.FileName;
                txtNamaFile.Text = namaFile.ToString();
                bmp = (Bitmap)pb1.Image.Clone();
                cekUji = true;
            }
        }

        private void menu_input_Load(object sender, EventArgs e)
        {
            namaFolder = @"D:\skripsi\dataset fix\";
            //openFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
        }

        private void btnKlasifikasi_Click(object sender, EventArgs e)
        {
            string txtSudut = cbPilihSudut.Text; //ambil sudut
            string metode = cbPilihMetode.Text; //ambil metode
            int sudut = 0; //sudut dalam int

            if (cekUji == true)
            {
                //pre-proses citra uji
                pictGrayScale = gray_scale(bmp); //grayscale
                pictConstrastS = contrast_stretching(pictGrayScale); //contrast_stretching
                dot = dot_segmentasi(pictConstrastS); //ambil dot

                if (metode == "GLCM") //kode =1
                {
                    if (txtSudut == "0" || txtSudut == "45" || txtSudut == "90" || txtSudut == "135")
                    {
                        sudut = int.Parse(txtSudut);
                        valueGlcm = fungsi_glcm(pictConstrastS, sudut, dot);
                        material = klasifikasi(1, sudut);
                    }
                    else
                        MessageBox.Show("Pilih Sudut Untuk Proses Ekstraksi", "Notice");
                }
                else if (metode == "GLRLM") //kode =2
                {
                    if (txtSudut == "0" || txtSudut == "45" || txtSudut == "90" || txtSudut == "135")
                    {
                        sudut = int.Parse(txtSudut);
                        valueGlrlm = fungsi_glrlm(pictConstrastS, sudut, dot);
                        material = klasifikasi(2, sudut);
                    }
                    else
                        MessageBox.Show("Pilih Sudut Untuk Proses Ekstraksi", "Notice");
                }
                else if (metode == "GLCM dan GLRLM") //kode=3
                {
                    if (txtSudut == "0" || txtSudut == "45" || txtSudut == "90" || txtSudut == "135")
                    {
                        sudut = int.Parse(txtSudut);
                        valueGlcm = fungsi_glcm(pictConstrastS, sudut, dot);
                        valueGlrlm = fungsi_glrlm(pictConstrastS, sudut, dot);
                        material = klasifikasi(3, sudut);
                    }
                    else
                        MessageBox.Show("Pilih Sudut Untuk Proses Ekstraksi", "Notice");
                }
                else
                    MessageBox.Show("Pilih Metode Ekstraksi!", "Notice");


                menu_hasil menuHasil = new menu_hasil(bmp, pictGrayScale, pictConstrastS, valueGlcm, valueGlrlm, material);
                menuHasil.Show();
            }
            else
                MessageBox.Show("Pilih Citra Uji!", "Notice");

            for (int i = 0; i < valueGlcm.Count(); i++)
                valueGlcm[i] = 0;
            for (int i = 0; i < valueGlrlm.Count(); i++)
                valueGlrlm[i] = 0;
        }

        } //public partial class
} //namespace