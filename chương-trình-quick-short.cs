using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chương_trình_quick_short
{
    internal class Program
    {
        // Hàm thực hiện thuật toán Quick Sort
        static void QuickSort(int[] mang, int trai, int phai)
        {
            if (trai < phai)
            {
                // Chia mảng thành hai phần bằng cách chọn phần tử chốt
                int chiSoChot = PhanHoach(mang, trai, phai);

                // Sắp xếp phần bên trái của chốt
                QuickSort(mang, trai, chiSoChot - 1);

                // Sắp xếp phần bên phải của chốt
                QuickSort(mang, chiSoChot + 1, phai);
            }
        }

        // Hàm phân hoạch mảng dựa trên phần tử chốt
        static int PhanHoach(int[] mang, int trai, int phai)
        {
            int chot = mang[phai]; // Chọn phần tử cuối cùng làm chốt
            int i = trai - 1; // Vị trí của phần tử nhỏ hơn chốt

            for (int j = trai; j < phai; j++)
            {
                if (mang[j] < chot)
                {
                    i++;
                    HoanDoi(mang, i, j); // Hoán đổi phần tử nhỏ hơn chốt về bên trái
                }
            }
            HoanDoi(mang, i + 1, phai); // Đưa chốt về đúng vị trí
            return i + 1;
        }

        // Hàm hoán đổi hai phần tử trong mảng
        static void HoanDoi(int[] mang, int a, int b)
        {
            int tam = mang[a];
            mang[a] = mang[b];
            mang[b] = tam;
        }

        static void Main(string[] args)
        {
            // Nhập số lượng phần tử trong mảng từ bàn phím
            Console.WriteLine("Nhập số lượng phần tử trong mảng:");
            int n = int.Parse(Console.ReadLine());
            int[] mang = new int[n];

            // Nhập các phần tử của mảng từ bàn phím
            Console.WriteLine("Nhập các phần tử của mảng:");
            for (int i = 0; i < n; i++)
            {
                mang[i] = int.Parse(Console.ReadLine());
            }

            // Gọi thuật toán Quick Sort để sắp xếp mảng
            QuickSort(mang, 0, mang.Length - 1);

            // In mảng sau khi sắp xếp
            Console.WriteLine("Mảng sau khi sắp xếp:");
            Console.WriteLine(string.Join(" ", mang));
        }
    }
}
