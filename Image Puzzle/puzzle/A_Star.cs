using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
	class State
	{
		List<int> trangThai;
		int g;
		int f;
		int hereustic;

		public State(List<int> trangThai)
		{
			this.trangThai = trangThai;
		}

		//hàm đếm các vị trí sai trong 1 trạng thái
		int DemSoOSaiTinhH(List<int> mangRandom, List<int>mangKetQua)
		{
			int demOSai = 0;
			for(int  i = 0; i < 9; i++)
			{
				if (mangRandom[i] != mangKetQua[i])
					demOSai++;
			}
			return demOSai;
		}

		//tính f = g + h
		int TinhFn(int g, State trangThaiKetQua)
		{
			this.g = g;
			this.hereustic = DemSoOSaiTinhH(this.trangThai,trangThaiKetQua.trangThai);
			this.f = g + this.hereustic;
			return f;
		}

		//hàm này để tách từ 1 mảng truyền vào theo mỗi bước đi(trái, phải, lên, xuống) sẽ tạo ra bao nhiêu mảng nữa
		public List<List<int>> PhatSinhMangTuMotMang(List<int> mangBatKy)
		{
			List<List<int>> mangPhatSinh = new List<List<int>>();
			int i = mangBatKy.IndexOf(9);	
			//để đi sang phải
			if (i + 3 < 9)
			{
				List<int> copy = new List<int>(mangBatKy);
				int temp = copy[i];
				copy[i] = copy[i + 1];
				copy[i + 1] = temp;
				mangPhatSinh.Add(copy);
			}
			//để đi sang trái
			if (i % 3 > 0)
			{
				List<int> copy = new List<int>(mangBatKy);
				int temp = copy[i];
				copy[i] = copy[i - 1];
				copy[i - 1] = temp;
				mangPhatSinh.Add(copy);
			}
			//để đi lên
			if (i - 3 >= 0)
			{
				List<int> copy = new List<int>(mangBatKy);
				int temp = copy[i];
				copy[i] = copy[i - 3];
				copy[i - 3] = temp;
				mangPhatSinh.Add(copy);
			}
			//để đi xuống
			if (i % 3 > 0)
			{
				List<int> copy = new List<int>(mangBatKy);
				int temp = copy[i];
				copy[i] = copy[i + 3];
				copy[i + 3] = temp;
				mangPhatSinh.Add(copy);
			}
			return mangPhatSinh;
		}
		public List<State> PhanTachTrangThai(State trangThaiKetQua)
		{
			List<State> phatSinh = new List<State>();
			List<List<int>> mangPhatSinh = PhatSinhMangTuMotMang(this.trangThai);
			for (int i = 0; i < mangPhatSinh.Count; i++)
			{
				State temp = new State(mangPhatSinh[i]);
				temp.TinhFn(this.g + 1, trangThaiKetQua);
			}




			return phatSinh;
		}
	}


	class algorithm
	{
		public List<State> duongDi;
		

		public List<State> Solve(List<int>trangThaiBanDau)
		{
			List<State> duongDi = null;

			// Thuat toan, tao ra duong di la lst state tu state this.begin den this.end

			return duongDi;
		}
	}

	class BFS
	{
		State begin, end;
		List<State> duongDi;

		public BFS(State begin, State end)
		{
			this.begin = begin;
			this.end = end;
			this.duongDi = null;
		}


		public List<State> Slove()
		{
			List<State> duongDi = null;

			// Thuat toan, tao ra duong di la lst state tu state this.begin den this.end

			return duongDi;
		}
	}


	//public void render(State a)
	//{
	//	a.mangTrangThai;
	//}

	//public void main()
	//{
	//	List<int> s = new List<int>(new int[] { 0, 9, 2, 3, 4, 5, 6, 7, 1 });

	//	List<int> e = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 9 });


	//	State begin = new State(s);
	//	State end = new State(e);


	//	//A_Star a = new A_Star(begin, end);

	//	BFS a = new BFS(begin, end);

	//	List<State> duongDi = a.Slove();


	//	// Render len man hinh
	//	//for (a each duongDi)
	//	//{
	//	//	render(a);
	//	//	spleep(2);
	//	//}
	//}

}


