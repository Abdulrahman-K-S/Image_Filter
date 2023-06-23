# Introduction  
This project tackles an important problem which deals with the removal of noise from images is a major task in the field of image processing, because it affects the quality of the image and leads to the loss of some of its important information through the impact of noise on it. Removing noise from an image is a big field and deals with many algorithms and techniques. Order statistic filters are non-linear spatial filters whose response is based on the ordering(ranking) of the pixels contained in the image area encompassed by the filter, and then replacing the value in the center pixel with the value determined by the ranking result. We are going to introduce and implement two of the order statistic filters which are:
1.	Alpha-Trim Filter
2.	Adaptive Median Filter

## Alpha-Trim Filter
Also known as the Alpha-Trimmed Mean Filter, it is a type of nonlinear filter used to remove noise from image data. It works by taking the average (mean) of a subset of pixel values, excluding the T highest and T lowest values in that subset; the T-value is taken as an input from the user. The number of pixels to include in the subset is determined by a parameter called the alpha value. The alpha-trimmed mean filter is effective at removing impulse noise, which is characterized by sudden, isolated spikes in pixel values. By excluding the T highest and T lowest values in the subset, the filter can remove these spikes while preserving the underlying structure of the image. The alpha-trimmed mean filter is a type of rank-order filter, which means that its output value is determined by the order of the pixel values in the subset, rather than their actual values. This makes it robust to outliers and other types of noise that can distort the pixel values in an image. The alpha-trimmed mean filter is commonly used in applications such as digital image processing, computer vision, and signal processing. Its effectiveness depends on the choice of alpha value, which should be selected based on the characteristics of the noise in the image.

## Adaptive Mean Filter
The adaptive mean filter is a type of nonlinear filter used for image processing and noise reduction. It works by calculating the mean value of a local neighborhood of pixels around each pixel in the image. Unlike a traditional mean filter, the size of the local neighborhood is not fixed, but varies depending on the local image characteristics.
To calculate the size of the local neighborhood, the adaptive mean filter uses a threshold value. If the difference between the central pixel and the mean of the local neighborhood is greater than the threshold value, then the size of the neighborhood is increased to include more pixels. This helps the filter to adapt to the local variation in the image data and preserve the edges and other fine details.
The adaptive mean filter is effective at removing noise from images while preserving the edges and other important features. It is particularly useful for images with non-uniform noise, where the noise level varies across the image. By adapting the size of the neighborhood based on the local image characteristics, the filter can remove noise without blurring or distorting the underlying image structure.
The adaptive mean filter is commonly used in applications such as medical imaging, remote sensing, and computer vision. Its effectiveness depends on the choice of threshold value and the size of the local neighborhood, which should be selected based on the characteristics of the image and the level of noise present.

## Alpha-Trim Filter – Sorting Algorithms: 
### Counting Sort:
Counting sort is an efficient sorting algorithm used to sort a collection of integers or other discrete values. It works by counting the number of occurrences of each value in the collection, and then using this information to determine the final sorted order.
Here's how counting sort works:
1.	Find the minimum and maximum values in the collection.
2.	Create an array of counters that is large enough to hold the range of values from the minimum to the maximum value.
3.	Iterate through the collection, incrementing the counter for each value encountered.
4.	Iterate through the counter array, accumulating the counts to determine the final position of each value in the sorted output.
5.	Iterate through the original collection again, placing each value in its final position in the sorted output.

The time complexity of counting sort is O(N + K), where n is the number of elements in the collection and k is the range of values. This makes it a linear time sorting algorithm, which can be very efficient for collections with a small range of values.
 
### Merge Sort:
Merge sort is a popular sorting algorithm that uses the divide-and-conquer approach to sort a collection of elements. It works by dividing the collection into two halves, recursively sorting each half, and then merging the two sorted halves into a single sorted output.
Here's how merge sort works:
1.	Divide the collection into two halves.
2.	Recursively sort each half.
3.	Merge the two sorted halves into a single sorted output.

The merge operation works by comparing the first element of each half, selecting the smaller one to be the next element in the output, and repeating the process until all elements have been merged. If one half is exhausted before the other, the remaining elements from the other half are appended to the output.
The time complexity of merge sort is O(N log N), where n is the number of elements in the collection. This makes it a very efficient sorting algorithm for large collections.
Merge sort is a stable sorting algorithm, meaning that it preserves the relative order of equal elements in the input collection. It is also an in-place sorting algorithm, meaning that it does not require additional memory to sort the collection.

## Adaptive Median Filter – Sorting Algorithms: 
### Insertion Sort:
Insertion sort is a simple sorting algorithm that is efficient for small collections or partially sorted collections. It works by iterating over the collection and inserting each element into its correct position in the sorted output.
Here's how insertion sort works:
1.	Iterate through the collection from left to right.
2.	For each element, compare it with the previous elements in the sorted output and insert it into its correct position.

To insert an element into the sorted output, we compare it with the previous elements from right to left, shifting each element to the right until we find the correct position for the new element. Once we have found the correct position, we insert the new element into that position.

The time complexity of insertion sort is O(N2), where n is the number of elements in the collection. This makes it less efficient than other sorting algorithms for large collections, but it can be very efficient for small or partially sorted collections.

The best case of insertion sort occurs when the input collection is already sorted or nearly sorted. In this case, the inner loop of the algorithm will never execute, and the time complexity of the algorithm becomes O(N), where n is the number of elements in the collection.
When the input collection is already sorted, the algorithm simply iterates through the collection once, comparing each element with the previous element and inserting it in its correct position. Since each element is already in its correct position, no elements need to be shifted to the right, and the inner loop of the algorithm is never executed.
Insertion sort is a stable sorting algorithm, meaning that it preserves the relative order of equal elements in the input collection. It is also an in-place sorting algorithm, meaning that it sorts the collection in place without requiring additional memory.
### Quick Sort:
Quick sort is a popular comparison-based sorting algorithm that uses the divide-and-conquer approach to sort a collection of elements. It works by partitioning the collection into two parts, based on a pivot element, and recursively sorting each part.
Here's how quick sort works:
1.	Choose a pivot element from the collection.
2.	Partition the collection into two parts: elements smaller than the pivot and elements larger than the pivot.
3.	Recursively sort each part.
4.	Combine the sorted parts into a single sorted output.

The partition step works by selecting a pivot element and dividing the collection into two parts: elements smaller than the pivot and elements larger than the pivot. This can be done in various ways, such as selecting the first, last, or middle element as the pivot, or selecting a random element as the pivot. Once the partition is complete, the pivot element is in its final sorted position.
The time complexity of quick sort is O(N log N) on average and O(N2) in the worst case, where n is the number of elements in the collection. The worst case occurs when the pivot element is consistently chosen in a way that leads to an unbalanced partition, such as selecting the first or last element of a sorted or nearly sorted collection.
Quick sort is a widely used sorting algorithm due to its efficiency and adaptability to different input distributions. It is also an in-place sorting algorithm, meaning that it sorts the collection in place without requiring additional memory.
## Alpha-Trim Filter - Complexity: 
It has two complexities due to the fact that the user chooses one of the two sorting algorithms, and each sorting algorithm has its complexity which is mentioned above. 
1. Using the Counting Sort Algorithm:
   - O(Length * Width * (Counting Sort + Average + N2) ) 
		= O(Length * Width * (N + K + N + N2) ) 
		= O(Length * Width * (N2 + K))
		- Length: It is the number of pixels on the x-axis that represent the length of the image to be processed.
		- Width: It is the number of pixels on the y-axis that represent the width of the image to be processed.
		- N: It is the number of the neighboring elements, also known as the WindowSize, in the collection.
		- K: It is the range of values.
		- Average: It is a function that calculates the average of the sorted array which is small compared to the whole algorithm, so it is ignored.

3.	Using the Merge Sort Algorithm:
	- O(Length * Width * (Merge Sort + Average + N2)) 
= O(Length * Width * ((N Log N) + N + N2))
= O(Length * Width * N2)
		- Length: It is the number of pixels on the x-axis that represent the length of the image to be processed.
		- Width: It is the number of pixels on the y-axis that represent the width of the image to be processed.
		- N: It is the number of the neighboring elements, also known as the WindowSize, in the collection.
		- Average: It is a function that calculates the average of the sorted array which is small compared to the whole algorithm, so it is ignored. 
## Adaptive Median Filter - Complexity: 
It has two complexities due to the fact that the user chooses one of the two sorting algorithms, and each sorting algorithm has its complexity which is mentioned above. 
1.	Using the Insertion Sort Algorithm: 
	- O(Length * Width * (WindowSize/2)* (Insertion Sort + N2) ) 
= O(Length * Width * (WindowSize/2)* (N2 + N2)) 
= O(Length * Width * WindowSize * N2)
		- Length: It is the number of pixels on the x-axis that represent the length of the image to be processed.
		- Width: It is the number of pixels on the y-axis that represent the width of the image to be processed.
		- WindowSize: It is the threshold value that is used to decide the number of neighbouring values, and it is divided by two because it gets incremented by 2 after every iteration. 
		- N: It is the number of the neighboring elements, also known as the WindowSize, in the collection.

2.	Using the Quick Sort Algorithm:
	- O(Length * Width * (WindowSize/2)* (Quick Sort + N2)) 
= O(Length * Width * (WindowSize/2)* (N2 + N2)) 
= O(Length * Width * WindowSize * N2)
		- Length: It is the number of pixels on the x-axis that represent the length of the image to be processed.
		- Width: It is the number of pixels on the y-axis that represent the width of the image to be processed.
		- WindowSize: It is the threshold value that is used to decide the number of neighbouring values, and it is divided by two because it gets incremented by 2 after every iteration. 
		- N: It is the number of the neighboring elements, also known as the WindowSize, in the collection.

# Which Sorting Algorithm is Better?
## Alpha-Trim Filter:

Counting Sort is a better Sorting Algorithm to be used in the Alpha-Trim Filter. Since “K” ranges from 0 to 255 (Constant), then it is clear that using either Counting Sort or Merge Sort, the complexity is the same. However, using Merge Sort, there are a lot more N’s, so it makes computation much slower than using Counting Sort. Through numerous trials, we found out that using Counting Sort takes considerably less time.

## Adaptive Median Filter:

Quick Sort is a better Sorting Algorithm to be used in the Adaptive Median Filter. Since Quick Sort’s average case is O(N log N) and Insertion Sort’s average case is O(N2), Quick Sort is the choice to go with! Noting that the Alpha Median Filter has the same complexity with each Sorting Algorithm, Quick Sort is likely to reach the average and/or best case than Insertion Sort is likely to reach its best case because Insertion Sort requires that the Array is sorted for it to be the best case which is not likely! Through numerous trials, we found out that using Quick Sort takes considerably less time.
