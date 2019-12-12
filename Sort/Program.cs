using System;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 1, 7, 6, 2, 3, 0, 8, 7, 5, 7, 0 };
            Console.WriteLine(string.Join(",", nums));
            //QuickSort(nums, 0, nums.Length - 1);
            //InsertSort(nums);
            //SelectSort(nums);
            //ShellSort(nums, nums.Length);
            HeapSort(nums, nums.Length);
            //Bubble(nums);
            Console.WriteLine(string.Join(",", nums));
        }


        //冒泡排序改进版
        static void Bubble(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                bool isLoop = false;//设置一个标记用于判断一轮循环是否有交换，如果没有表示数组已经有序
                for (int j = 0; j < nums.Length - 1 - i; j++)
                {
                    if (nums[j]>nums[j+1])
                    {
                        Swap(ref nums[j], ref nums[j + 1]);
                        isLoop = true;
                    }
                }

                //如果一轮循环没有交换那么数组已经有序
                if (isLoop == false)
                {
                    break;
                }

            }

        }


        //选择排序:找到最小的数与第一个元素交换，接着从剩下的元素中继续这种操作，最终的得到一个有序序列
        static void SelectSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int index = i;
                //循环找到最小的一个数
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[index] > arr[j])
                    {
                        index = j;
                    }
                }
                if (index != i)
                {
                    //交换
                    Swap(ref arr[index], ref arr[i]);
                }
            }
        }



        //插入排序：将未排序序列的第一个数与已经有序的序列逐一比较，
        //如果有序序列的数大于无序序列的数,有序序列的这个数后移一个,
        //直到有序序列的数不大于无序序列的数,那么这个无序序列的数就已经有序了
        static void InsertSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int index = i - 1;//已经有序的个数
                int currValue = arr[i];//无序序列的第一个数
                //无序序列的第一个数与有序序列比较,如果无序序列的第一个数小于有序序列的数,那么就将有序序列的数后移一个
                while (index >=0 && arr[index] > currValue)
                {
                    arr[index + 1] = arr[index];
                    index--;
                }
                //循环一次可以将无序序列的第一个数找到适合的位置插入
                arr[index + 1] = currValue;
            }
        }




        //快速排序
        static void QuickSort(int[] nums,int left,int right)
        {
            if (left >= right)
            {
                return;
            }

            //分治法将数组分为两份
            int mid = GetIndex(nums, left, right);
            //对小于基准元素的数组部分排序
            QuickSort(nums, left, mid - 1);
            //对大于基准元素的数组排序
            QuickSort(nums, mid + 1, right);
        }

        //分治法将数组分成两份
        static int GetIndex(int[] nums,int left,int right)
        {
            //数组的第一个元素作为基准元素
            int temp = nums[left];
            while (left < right)
            {
                //从元素末尾遍历数组，如果元素大于基准元素下标减1，
                while (left < right && nums[right] >= temp)
                {
                    right--;
                }
                //交换
                nums[left] = nums[right];

                //从数组的开头开始遍历数组,如果元素小于基准元素下标加1,
                while (left < right && nums[left] <= temp)
                {
                    left++;
                }
                //交换
                nums[right] = nums[left];
            }
            //找到了基准元素的位置,将基准元素放到这个位置
            nums[left] = temp;
            return left;
        }


        //希尔排序
        static void ShellSort(int[] arr,int length)
        {
            //组内间隔
            for (int interval = length / 2; interval > 0; interval /= 2)
            {
                //每组的个数
                for (int n = 0; n < interval; n++)
                {
                    //组内插入排序
                    for (int i = n + interval; i < length; i += interval)
                    {
                        int index = arr[i];
                        int j = i - interval;

                        while (j >= 0 && arr[j] > index)
                        {
                            arr[j + interval] = arr[j];
                            j = j - interval;
                        }

                        arr[j + interval] = index;
                    }
                }
            }
        }



        //堆排序
        static void HeapSort(int[] nums,int n)
        {
            //创建大顶堆,
            //将数组中的中间一个数最为堆的根节点,因此不会出现节点没有左子树的情况,所以在创建大顶堆时不需要判断是否存在左子树
            CreateHeap(nums, n / 2 - 1, n);

            for (int i = n - 1; i >= 0; i--)
            {

                //将数组的第一个数与最后一个数交换,创建一次大顶堆,可以将数组中的最大数移到数组的第一个位置
                Swap(ref nums[0], ref nums[i]);

                //创建大顶堆
                CreateHeap(nums, i / 2 - 1, i);
            }
        }

        //创建大顶堆:i为堆的根节点,n为堆的大小
        static void CreateHeap(int[] nums,int i,int n)
        {
            while (i>=0)
            {
                int left = i * 2 + 1;//左子树节点
                int right = i * 2 + 2;//右子树节点
                int j = 0;//记录左子树和右子树中较大的节点的下标
                //存在右子树
                if (right < n)
                {
                    j = nums[left] > nums[right] ? left : right;
                }
                //不存在右子树
                else
                {
                    j = left;
                }
                if (nums[i]<nums[j])
                {
                    Swap(ref nums[i], ref nums[j]);
                }

                i--;
            }
        }


        //交换两个数
        static void Swap(ref int a, ref int b)
        {
            //if (a == b)
            //{
            //    return;
            //}
            int temp = a;
            a = b;
            b = temp;


            //a = a ^ b;
            //b = a ^ b;
            //a = a ^ b;


            //a = a + b;
            //b = a - b;
            //a = a - b;

        }


    }
}
