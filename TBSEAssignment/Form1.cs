using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBSEAssignment
{
    public partial class Form1 : Form
    {
        Boolean colorEffectFlag = false;
        Image beforeColorEffect;
        int effectCounter = 1;
        Image revertImage;
        Image redoImage;
        Image originalImage;
        Image currentImage;
        Bitmap currentColorImg;
        Bitmap colorImg;
        public CancellationTokenSource cancelTokenSource;
        public CancellationToken cancelToken;


        public Form1()
        {
            InitializeComponent();

        }
        Image convertToGrayScale()
        {
            Bitmap grayImg;
            Image grayScaleImage = null;

            revertImage = mainPictureBox.Image;
            currentImage = mainPictureBox.Image;
            currentColorImg = new Bitmap(currentImage);

            grayImg = new Bitmap(currentImage.Width, currentImage.Height);
            Color currentColor, newColor;
            for (int i = 0; i < currentImage.Width; i++)
            {
                for (int j = 0; j < currentImage.Height; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    currentColor = currentColorImg.GetPixel(i, j);
                    int newColorA = (int)currentColor.A;
                    int newColorGray = (int)((Convert.ToDouble(currentColor.R) + Convert.ToDouble(currentColor.G) + Convert.ToDouble(currentColor.B)) / 3);
                    newColor = Color.FromArgb(newColorA, newColorGray, newColorGray, newColorGray);
                    grayImg.SetPixel(i, j, newColor);
                }
            }
            grayScaleImage = (Image)grayImg;
            return grayScaleImage;
        }
        Image convertToBlur()
        {
            Bitmap blurImg;
            Image blurImage = null;

            revertImage = mainPictureBox.Image;
            currentImage = mainPictureBox.Image;
            currentColorImg = new Bitmap(currentImage);
            blurImg = new Bitmap(currentImage.Width, currentImage.Height);
            Color prevX, nextX, prevY, nextY;
            int blurStrength = 2;
            for (int x = blurStrength; x < currentImage.Width - blurStrength; x++)
            {
               

                for (int y = blurStrength; y < currentImage.Height - blurStrength; y++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    prevX = currentColorImg.GetPixel(x - blurStrength, y);
                    nextX = currentColorImg.GetPixel(x + blurStrength, y);
                    prevY = currentColorImg.GetPixel(x, y - blurStrength);
                    nextY = currentColorImg.GetPixel(x, y + blurStrength);

                    int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                    int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                    int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);
                    blurImg.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }

            }
            blurImage = (Image)blurImg;

            return blurImage;
        }
        Image convertToMirror()
        {
            Bitmap mirrorImg;
            Image mirrorImage = null;


            revertImage = mainPictureBox.Image;
            currentImage = mainPictureBox.Image;
            currentColorImg = new Bitmap(currentImage);
            mirrorImg = new Bitmap(currentImage.Width, currentImage.Height);
            Color pix;
            int width = currentImage.Width - 1;
            for (int x = 1; x < currentImage.Width; x++)
            {
                for (int y = 1; y < currentImage.Height; y++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pix = currentColorImg.GetPixel(x, y);
                    mirrorImg.SetPixel(width, y, pix);
                }
                width--;
            }
            mirrorImage = (Image)mirrorImg;

            return mirrorImage;
        }
        Image convertToRed()
        {
            Bitmap redImg;
            Image redImage = null;

            currentImage = mainPictureBox.Image;
            if (colorEffectFlag)
            {
                if (effectCounter == 2)
                {
                    currentColorImg = new Bitmap(beforeColorEffect);
                }
                else
                {
                    currentColorImg = new Bitmap(revertImage);
                }
                revertImage = mainPictureBox.Image;
            }

            else
            {
                currentColorImg = new Bitmap(currentImage);
                revertImage = mainPictureBox.Image;
                colorEffectFlag = true;
                beforeColorEffect = revertImage;
                effectCounter++;
            }

            redImg = new Bitmap(currentImage.Width, currentImage.Height);
            Color pixel, redPixel;
            for (int i = 1; i < currentImage.Width; i++)
            {
                for (int j = 1; j < currentImage.Height; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pixel = currentColorImg.GetPixel(i, j);
                    redPixel = Color.FromArgb(pixel.A, pixel.R, 0, 0);
                    redImg.SetPixel(i, j, redPixel);
                }
            }

            redImage = (Image)redImg;

            return redImage;
        }
        Image convertToGreen()
        {

            Bitmap greenImg;
            Image greenImage = null;

            currentImage = mainPictureBox.Image;
            if (colorEffectFlag)
            {
                if (effectCounter == 2)
                {
                    currentColorImg = new Bitmap(beforeColorEffect);
                }
                else
                {
                    currentColorImg = new Bitmap(revertImage);
                }
                revertImage = mainPictureBox.Image;
            }

            else
            {
                currentColorImg = new Bitmap(currentImage);
                revertImage = mainPictureBox.Image;
                colorEffectFlag = true;
                beforeColorEffect = revertImage;
                effectCounter++;
            }

            greenImg = new Bitmap(currentImage.Width, currentImage.Height);
            Color pixel, greenPixel;
            for (int i = 1; i < currentImage.Width; i++)
            {
                for (int j = 1; j < currentImage.Height; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pixel = currentColorImg.GetPixel(i, j);
                    greenPixel = Color.FromArgb(pixel.A, 0, pixel.G, 0);
                    greenImg.SetPixel(i, j, greenPixel);
                }
            }

            greenImage = (Image)greenImg;
            return greenImage;
        }
        Image convertToBlue()
        {

            Bitmap blueImg;
            Image blueImage = null;

            currentImage = mainPictureBox.Image;
            if (colorEffectFlag)
            {
                if (effectCounter == 2)
                {
                    currentColorImg = new Bitmap(beforeColorEffect);
                }
                else
                {
                    currentColorImg = new Bitmap(revertImage);
                }
                revertImage = mainPictureBox.Image;
            }

            else
            {
                currentColorImg = new Bitmap(currentImage);
                revertImage = mainPictureBox.Image;
                colorEffectFlag = true;
                beforeColorEffect = revertImage;
                effectCounter++;
            }

            blueImg = new Bitmap(currentImage.Width, currentImage.Height);
            Color pixel, bluePixel;
            for (int i = 1; i < currentImage.Width; i++)
            {
                for (int j = 1; j < currentImage.Height; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pixel = currentColorImg.GetPixel(i, j);
                    bluePixel = Color.FromArgb(pixel.A, 0, 0, pixel.B);
                    blueImg.SetPixel(i, j, bluePixel);
                }
            }

            blueImage = (Image)blueImg;
            return blueImage;
        }
        Image convertToInverted()
        {

            Bitmap invertedImg;
            Image invertedImage = null;
            revertImage = mainPictureBox.Image;
            currentImage = mainPictureBox.Image;
            currentColorImg = new Bitmap(currentImage);

            invertedImg = new Bitmap(currentImage.Width, currentImage.Height);
            Color pixel;
            for (int i = 1; i < currentImage.Width; i++)
            {
                for (int j = 1; j < currentImage.Height; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pixel = currentColorImg.GetPixel(i, j);
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;
                    invertedImg.SetPixel(i, j, Color.FromArgb(255 - red, 255 - green, 255 - blue));
                }
            }
            invertedImage = (Image)invertedImg;

            return invertedImage;

        }
        Image convertToRotateLeft()
        {

            Bitmap rotateImg;
            Image rotateImage = null;

            revertImage = mainPictureBox.Image;
            currentImage = mainPictureBox.Image;
            currentColorImg = new Bitmap(currentImage);


            rotateImg = new Bitmap(currentImage.Height, currentImage.Width);
            int width = rotateImg.Height;
            Color pixel;

            for (int i = 1; i < currentImage.Width; i++)
            {
                for (int j = 1; j < currentImage.Height; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pixel = currentColorImg.GetPixel(i, j);

                    rotateImg.SetPixel(j, currentImage.Width - i, pixel);
                }

            }
            rotateImage = (Image)rotateImg;

            return rotateImage;
        }
        Image convertToRotateRight()
        {

            Bitmap rotateImg;
            Image rotateImage = null;

            revertImage = mainPictureBox.Image;
            currentImage = mainPictureBox.Image;
            currentColorImg = new Bitmap(currentImage);


            rotateImg = new Bitmap(currentImage.Height, currentImage.Width);
            int width = rotateImg.Height;
            Color pixel;

            for (int i = 1; i < currentImage.Width; i++)
            {
                for (int j = 1; j < currentImage.Height; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pixel = currentColorImg.GetPixel(i, j);

                    rotateImg.SetPixel(currentImage.Height - j, i, pixel);
                }

            }
            rotateImage = (Image)rotateImg;

            return rotateImage;
        }
        Image convertToCropped()
        {

            Bitmap cropImg;
            Image cropImage = null;

            revertImage = mainPictureBox.Image;
            currentImage = mainPictureBox.Image;
            currentColorImg = new Bitmap(currentImage);
            int croppedHeight = (int)(currentImage.Height * 0.90);
            int croppedWidth = (int)(currentImage.Width * 0.90);


            cropImg = new Bitmap(croppedWidth, croppedHeight);
            Color pixel;

            for (int i = 0; i < croppedWidth; i++)
            {

                for (int j = 0; j < croppedHeight; j++)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    pixel = currentColorImg.GetPixel(i, j);

                    cropImg.SetPixel(i, j, pixel);

                }

            }
            cropImage = (Image)cropImg;


            return cropImage;
        }
        void revertChange()
        {
            redoImage = mainPictureBox.Image;
            mainPictureBox.Image = revertImage;
        }
        void resetImage()
        {
            mainPictureBox.Image = originalImage;
            colorEffectFlag = false;
            effectCounter = 1;
            revertImage = originalImage;
            beforeColorEffect = originalImage;

        }
        void redoChange()
        {
            mainPictureBox.Image = redoImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            openFileDialog1.ShowDialog();
            String imageFile = openFileDialog1.FileName;
            try
            {
                originalImage = Image.FromFile(imageFile);
                colorImg = new Bitmap(originalImage);
                revertImage = originalImage;
                redoImage = originalImage;
                mainPictureBox.Image = originalImage;
                openImageIcon.Visible = false;
                label2.Visible = false;
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error: Image did not load. Please try again! ");
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Error: Image did not load. Please try again! ");
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Error: Image did not load. Please try again! ");
            }


        }

        private void grayBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToGrayScale);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnRanToCompletion,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                  
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());


        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            Action action = new Action(revertChange);
            Task task = new Task(action);
            task.Start(); TaskScheduler.FromCurrentSynchronizationContext();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            Action action = new Action(resetImage);
            Task task = new Task(action);
            task.Start(); TaskScheduler.FromCurrentSynchronizationContext();

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            try
            {
                int filterIndex = saveFileDialog1.FilterIndex;
                if (filterIndex == 1)
                {
                    mainPictureBox.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (filterIndex == 2)
                {
                    mainPictureBox.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                MessageBox.Show("Image Successfully Saved! ");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Image saving failed!");
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show("Image saving failed!");
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Please import image first");
            }



        }

        private void blurBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToBlur);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
           CancellationToken.None,
           TaskContinuationOptions.OnlyOnRanToCompletion,
           TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                    
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.NotOnRanToCompletion,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void mirrorBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToMirror);
            Task<Image> task = new Task<Image>(func,cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
           CancellationToken.None,
           TaskContinuationOptions.OnlyOnRanToCompletion,
           TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
              
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void redBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToRed);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
           CancellationToken.None,
           TaskContinuationOptions.OnlyOnRanToCompletion,
           TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                    
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void greenBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToGreen);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnRanToCompletion,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                   
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void blueBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToBlue);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
           CancellationToken.None,
           TaskContinuationOptions.OnlyOnRanToCompletion,
           TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                    
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void invertBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToInverted);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnRanToCompletion,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                    
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void rotateBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToRotateLeft);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
           CancellationToken.None,
           TaskContinuationOptions.OnlyOnRanToCompletion,
           TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!"); ;
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                   
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());


        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToRotateRight);
            Task<Image> task = new Task<Image>(func, cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
           CancellationToken.None,
           TaskContinuationOptions.OnlyOnRanToCompletion,
           TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                   
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void redoBtn_Click(object sender, EventArgs e)
        {
            Action action = new Action(redoChange);
            Task task = new Task(action);
            task.Start(); TaskScheduler.FromCurrentSynchronizationContext();
            
        }

        private void cropBtn_Click(object sender, EventArgs e)
        {
            cancelTokenSource = new CancellationTokenSource();
            cancelToken = cancelTokenSource.Token;
            Func<Image> func = new Func<Image>(convertToCropped);
            Task<Image> task = new Task<Image>(func,cancelToken);
            task.Start();
            task.ContinueWith((task) =>
            {
                mainPictureBox.Image = task.Result;
            },
           CancellationToken.None,
           TaskContinuationOptions.OnlyOnRanToCompletion,
           TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                AggregateException agrex = task.Exception;
                agrex.Handle((ex) =>
                {
                    bool ret = false;
                    if (agrex.InnerException is InvalidOperationException)
                    {
                        MessageBox.Show("Error: Image in use. Please wait!");
                        ret = true;
                    }
                    else if (agrex.InnerException is NullReferenceException)
                    {
                        MessageBox.Show("Error: No image loaded!");
                        ret = true;
                    }
                    else if (agrex.InnerException is ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error : Press reset Image");
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                    
                    return ret;
                });
            },
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted,
            TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith((task) =>
            {
                MessageBox.Show("Task was cancelled");
            }, CancellationToken.None,
             TaskContinuationOptions.OnlyOnCanceled,
             TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (mainPictureBox.Image == null)
                {
                    MessageBox.Show("No image to copy. Please load image.");

                }
                else
                {
                    Image img = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);

                    Graphics g = Graphics.FromImage(img);

                    g.CopyFromScreen(PointToScreen(mainPictureBox.Location), new Point(0, 0), new Size(mainPictureBox.Width, mainPictureBox.Height));

                    Clipboard.SetImage(img);

                    g.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry. An error occured while copying to clipboard.");
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            try { cancelTokenSource.Cancel(); }
            catch (Exception ex) 
            {
                MessageBox.Show("No tasks to cancel");
            }
        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }
    }
}
