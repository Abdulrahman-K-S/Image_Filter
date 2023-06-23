using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageFilters
{
    public class ImageOperations
    {
        /// <summary>
        /// Open an image, convert it to gray scale and load it into 2D array of size (Height x Width)
        /// </summary>
        /// <param name="ImagePath">Image file path</param>
        /// <returns>2D array of gray values</returns>
        public static byte[,] OpenImage(string ImagePath)
        {
            Bitmap original_bm = new Bitmap(ImagePath);
            int Height = original_bm.Height;
            int Width = original_bm.Width;

            byte[,] Buffer = new byte[Height, Width];

            unsafe
            {
                BitmapData bmd = original_bm.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, original_bm.PixelFormat);
                int x, y;
                int nWidth = 0;
                bool Format32 = false;
                bool Format24 = false;
                bool Format8 = false;

                if (original_bm.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    Format24 = true;
                    nWidth = Width * 3;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format32bppArgb || original_bm.PixelFormat == PixelFormat.Format32bppRgb || original_bm.PixelFormat == PixelFormat.Format32bppPArgb)
                {
                    Format32 = true;
                    nWidth = Width * 4;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    Format8 = true;
                    nWidth = Width;
                }
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (y = 0; y < Height; y++)
                {
                    for (x = 0; x < Width; x++)
                    {
                        if (Format8)
                        {
                            Buffer[y, x] = p[0];
                            p++;
                        }
                        else
                        {
                            Buffer[y, x] = (byte)((int)(p[0] + p[1] + p[2]) / 3);
                            if (Format24) p += 3;
                            else if (Format32) p += 4;
                        }
                    }
                    p += nOffset;
                }
                original_bm.UnlockBits(bmd);
            }

            return Buffer;
        }

        /// <summary>
        /// Get the height of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeight(byte[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidth(byte[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(1);
        }

        /// <summary>
        /// Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <param name="PicBox">PictureBox object to display the image on it</param>
        public static void DisplayImage(byte[,] ImageMatrix, PictureBox PicBox)
        {
            // Create Image:
            //==============
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            Bitmap ImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[0] = p[1] = p[2] = ImageMatrix[i, j];
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }
            PicBox.Image = ImageBMP;
        }

        // The Sorting Functions

        public static void CountingSort(byte[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            // find the maximum value in the array
            byte maxVal = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > maxVal)
                {
                    maxVal = arr[i];
                }
            }

            // create a count array to keep track of the frequency of each value
            int[] count = new int[maxVal + 1];

            // loop through the array and count the frequency of each value
            for (int i = 0; i < arr.Length; i++)
            {
                count[arr[i]]++;
            }

            // update the count array to keep track of the running sum
            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }

            // create a temporary array to store the sorted values
            byte[] result = new byte[arr.Length];

            // loop through the array in reverse order and place each value in its sorted position
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                result[count[arr[i]] - 1] = arr[i];
                count[arr[i]]--;
            }

            // copy the sorted values back into the original array
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = result[i];
            }
        }

        public static void Merge(byte[] arr, int start, int end, int mid)
        {

            int i = start, j = mid + 1, k = start;
            byte[] new_arr = new byte[1000]; /// array to save sorted elements
                                 /// loop until one of the two smallest array finsih
            while (i <= mid && j <= end)
            {
                // checking which element in turn in each array is less than the other
                // And adding it to the sorted array
                if (arr[i] <= arr[j])
                {
                    new_arr[k] = arr[i];
                    i++;
                }
                else
                {
                    new_arr[k] = arr[j];
                    j++;
                }
                k++;
            }
            /// if the first array doesn't end add the rest of it's element
            while (i <= mid)
            {
                new_arr[k] = arr[i];
                i++; k++;
            }
            /// if the second array doesn't end add the rest of it's element
            while (j <= end)
            {
                new_arr[k] = arr[j];
                j++; k++;
            }
            // copy all elements
            for (int x = start; x < k; x++)
                arr[x] = new_arr[x];
        }

        public static void MergeSort(byte[] arr, int start, int end)
        {
            if (start >= end)
                return;
            int mid = (start + end) / 2; // (Or) start + (end - start)/2
            MergeSort(arr, start, mid); // sort first half
            MergeSort(arr, mid + 1, end); // sort first half
            Merge(arr, start, end, mid); /// combine both subarrays
        }

        public static void Swap(byte[] neighbors, int index1, int index2)
        {
            // Swap function to swap the values of an array at given indeces
            byte temp = neighbors[index1];
            neighbors[index1] = neighbors[index2];
            neighbors[index2] = temp;
        }
        public static void InsertionSort(byte[] window, int size)
        {
            for (int i = 1; i <= size; i++)
            {
                int index = i; //Choosing the elements to be the pivot one by one
                for (int j = i - 1; j >= 0; j--)
                {
                    if (window[index] <= window[j])
                    {
                        Swap(window, index, j); // Swapping if a smaller element is found
                        index--;
                    }
                    else
                        break;
                }
            }
        }

        public static int Partition(byte[] neighbors, int start, int end)
        {
            // This function puts the pivot at its right place in the array
            // Where the elements before it are smaller and after it are larger
            // Finally it returns the index of the pivot
            int p = start, i = start, j = end;
            while (i < j)
            {
                while (neighbors[i] <= neighbors[p] && i < j)
                    i++;
                while (neighbors[j] > neighbors[p])
                    j--;
                if (i < j)
                    Swap(neighbors ,i, j);
            }
            Swap(neighbors, j, p);
            return j;
        }

        public static void QuickSort(byte[] neighbors, int start, int end)
        {
            if (start >= end)
                return;
            int p = Partition(neighbors, start, end); // Partition function returns pivot index
            QuickSort(neighbors, start, p - 1); // sort elements before pivot
            QuickSort(neighbors, p + 1, end); // sort elements after pivot
        }

        public static int getNeighbors(byte[,] ImageMatrix, byte[] neighbors /*Neighbors Array*/,int x /*rowNo*/, int y /*columnNo*/, int WindowSize)
        {
            // This function is aims for adding the neighbors of a given element in the ImageMatrix
            // to the neighbors array and returns the number of neighbors
            int neighbors_num = 0;
            for (int i = x - (WindowSize / 2); i <= x + (WindowSize / 2); i++)
            {
                if (i < 0 || i >= ImageMatrix.GetLength(0)) // checking if the given index is out of bounds
                    continue;
                for (int j = y - (WindowSize / 2); j <= y + (WindowSize / 2); j++)
                {
                    if (j < 0 || j >= ImageMatrix.GetLength(1)) // checking if the given index is out of bounds
                        continue;
                    neighbors[neighbors_num] = ImageMatrix[i,j];
                    neighbors_num++;
                }
            }
            return neighbors_num;
        }

        public static int GetAverage(byte[] window, int start, int end)
        {
            //Getting the average byte number in the window array
            int average = 0;
            int size = 0;

            for (int i = start; i <= end; i++)
            {
                average += window[i];
                size++;
            }

            if (size == 0)
                return average / (size + 1);
            return average / size;
        }

        public static byte [,] AlphaTrimFilter(byte[,] ImageMatrix,int WindowSize,int T,int Type)
        {
            // write your code here
            // add any functions but don't change or remove anything

            /* 
             * *STEPS* 
             * 1) Store the values of the neighboring pixels in an array. The array is called the window,
             *    and it should be odd sized.
             * 2) Sort the values in the window in ascending order.
             * 3) Exclude the first T values (smallest) and the last T values (largest) from the array.
             * 4) Calculate the average of the remaining values as the new pixel value and place it in the
             *    center of the window in the new image.
             */
            
            
            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {

                    byte[] neighbors = new byte[100]; // neighbors array

                    // returning the number of neighbors in a variable after adding the neighbors in the neighbors array
                    int neighbors_num = getNeighbors(ImageMatrix, neighbors, i, j, WindowSize); 

                    //putting the neighbors in a window array of the exact size of number of neigbors
                    byte[] window = new byte[neighbors_num];
                    for (int p = 0; p < neighbors_num; p++)
                        window[p] = neighbors[p];

                    if (Type == 1)
                    {
                        // Use Counting Sort
                        CountingSort(window);
                    }
                    else if (Type == 2)
                    {
                        // Use Merge Sort
                        MergeSort(window, 0, neighbors_num-1);
                    }
                    
                    //calculating average
                    int Average = GetAverage(window, T, neighbors_num-T-1);
                    //Assignening the average number to the byte
                    ImageMatrix[i, j] = (byte) Average;
                }
            }
            //throw new NotImplementedException(); // comment this line
            return ImageMatrix;
        }
        public static byte [,] MedianFilter(byte[,] ImageMatrix,int WS,int Type)
        {
            // write your code here
            // add any functions but don't change or remove anything

            /* 
             * *STEPS*
             * 1) Start by a window 3x3
             * 2) Choose a non-noise median value
             * 3) Replace the center with the median value or leave it 
             * 4) Repeat the process for the next pixel starting from step 1 again
             */

            for (int i = 0; i < ImageMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ImageMatrix.GetLength(1); j++)
                {
                    for (int w = 3; w <= WS; w+=2) // this loop if for increasing the window size
                    {
                        byte[] neighbors = new byte[10000]; // neighbors array

                        // returning the number of neighbors in a variable after adding the neighbors in the neighbors array
                        int neighbors_num = getNeighbors(ImageMatrix, neighbors, i, j, w);

                        //putting the neighbors in a window array of the exact size of number of neigbors
                        byte[] window = new byte[neighbors_num];
                        for (int p = 0; p < neighbors_num; p++)
                            window[p] = neighbors[p];

                        if (Type == 1)
                        {
                            // Use Insertion Sort
                            InsertionSort(window, neighbors_num - 1);
                        }
                        else if (Type == 2)
                        {
                            // Use Quick Sort
                            QuickSort(window, 0, neighbors_num - 1);
                        }

                        byte Zxy = ImageMatrix[i, j]; //Putting the byte on work in a variable
                        byte Zmax = window[neighbors_num - 1]; //Maximum value in the sorted array
                        byte Zmin = window[0]; //Minimum value in the sorted array
                        byte Zmed = window[((neighbors_num - 1) / 2)]; //Meduim value of the array

                        int A1 = Zmed - Zmin, A2 = Zmax - Zmed;
                        if (A1 < 0 && A2 < 0)
                        {
                            if (w + 2 <= WS)
                                continue;
                            else
                                ImageMatrix[i, j] = Zxy;
                        }
                        else
                        {
                            int B1 = Zxy - Zmin, B2 = Zmax - Zxy;
                            if (B1 > 0 && B2 > 0)
                                ImageMatrix[i, j] = Zxy;
                            else
                                ImageMatrix[i, j] = Zmed;
                        }
                    }
                }
            }

            //throw new NotImplementedException(); // comment this line
            return ImageMatrix;
        }
    }
}
