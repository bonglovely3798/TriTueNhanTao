//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace WindowsFormsApplication1
//{
//	class State
//	{
//		List<int> trangThai;

//		public State(List<int> trangThai)
//		{
//			this.trangThai = trangThai;
//		}

//		//hàm này để tách từ 1 mảng truyền vào theo mỗi bước đi(trái, phải, lên, xuống) sẽ tạo ra bao nhiêu mảng nữa
//		public List<List<int>> PhatSinhMangTuMotMang(List<int> mangBatKy)
//		{
//			List<List<int>> mangPhatSinh = new List<List<int>>();
//			int i = mangBatKy.IndexOf(9);	
//			//để đi sang phải
//			if (i % 3 < 2)
//			{
//				List<int> copy = new List<int>(mangBatKy);
//				int temp = copy[i];
//				copy[i] = copy[i + 1];
//				copy[i + 1] = temp;
//				mangPhatSinh.Add(copy);
//			}
//			//để đi sang trái
//			if (i % 3 > 0)
//			{
//				List<int> copy = new List<int>(mangBatKy);
//				int temp = copy[i];
//				copy[i] = copy[i - 1];
//				copy[i - 1] = temp;
//				mangPhatSinh.Add(copy);
//			}
//			//để đi lên
//			if (i - 3 >= 0)
//			{
//				List<int> copy = new List<int>(mangBatKy);
//				int temp = copy[i];
//				copy[i] = copy[i - 3];
//				copy[i - 3] = temp;
//				mangPhatSinh.Add(copy);
//			}
//			//để đi xuống
//			if (i + 3 < mangBatKy.Count)
//			{
//				List<int> copy = new List<int>(mangBatKy);
//				int temp = copy[i];
//				copy[i] = copy[i + 3];
//				copy[i + 3] = temp;
//				mangPhatSinh.Add(copy);
//			}
//			return mangPhatSinh;
//		}
//		//đóng gói tất cả mảng có thể phát sinh từ 1 mảng truyền vào (là hàm phía trên) thành 1 state
//		public List<State> PhanTachTrangThai()
//		{
//			List<State> phatSinh = new List<State>();
//			List<List<int>> mangPhatSinh = PhatSinhMangTuMotMang(this.trangThai);
//			for (int i = 0; i < mangPhatSinh.Count; i++)
//			{
//				State dongGoi = new State(mangPhatSinh[i]);
//				phatSinh.Add(dongGoi);

//			}
//			return phatSinh;
//		}
//		public bool KiemTraDenDich(State mangGoc)
//		{
//			bool denDich = true;
		
//			for (int i = 0; i < mangGoc.trangThai.Count; i++)
//			{
//				if (this.trangThai[i] != mangGoc.trangThai[i])
//				{
//					denDich = false;
//					break;
//				}
//			}
//			return denDich;
//		}
//	}

//	class BFS
//	{
//		State trangThaiDau;
//		State trangThaiDich;

//		public BFS(State trangThaiDau, State trangThaiDich)
//		{
//			this.trangThaiDau = trangThaiDau;
//			this.trangThaiDich = trangThaiDich;
//		}


//		public List<State> Solve()
//		{
//			List<State> ketQua = new List<State>();

//			Queue<State> danhSachL = new Queue<State>();
//			danhSachL.Enqueue(trangThaiDau);

//			while(danhSachL.Count > 0)
//			{
//				State ptLayRa = danhSachL.Dequeue();
//				List<State> hungPhanTachTrangThai = ptLayRa.PhanTachTrangThai();
//				foreach(var item in hungPhanTachTrangThai)
//				{
//					if (item.KiemTraDenDich(trangThaiDich) == true)
//					{
//						Console.WriteLine("Ban da chien thang!.....");
//						break;
//					}
//					else
//					{
//						danhSachL.Enqueue(item);
//					}
//				}
//			}
//			return ketQua;
//		}
//	}

//}


