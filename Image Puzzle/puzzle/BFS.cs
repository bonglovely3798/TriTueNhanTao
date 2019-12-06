using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BFS
{
	public partial class Node
	{
		//danh sách các trạng thái được tạo ra từ 1 trạng thái ban đầu
		public List<Node> nutCon = new List<Node>();
		//nút cha là để mỗi trạng thái cha sinh ra các trạng thái con, rồi từ đó bắt ra đi tiếp
		public Node nutCha;
		public List<int> mangGoc = new List<int>(9);
		//x để ghi lại chỉ số ô đen trống
		public int x = 0;
		int col = 3;

		//khởi tạo với tham số truyền vào là một mảng kiểu int 
		public Node(List<int> p)
		{

		}

		public void setPuzzle(List<int> p)
		{
			for(int i = 0; i < mangGoc.Count; i++)
			{
				this.mangGoc[i] = p[i];
			}
		}

		public void DiQuanPhai(List<int> p, int i)
		{
			if(i % col < col - 1)
			{
				List<int> pc = new List<int>(9);
				CopyPuzzle(pc, p);

				int temp = pc[i + 1];
				pc[i + 1] = pc[i];
				pc[i] = temp;

				Node con = new Node(pc);
				nutCon.Add(con);
				con.nutCha = this;
			}
		}
		public void DiQuanTrai(List<int> p, int i)
		{
			if(i % col > 0)
			{
				List<int> pc = new List<int>();
				CopyPuzzle(pc, p);
				int temp = pc[i - 1];
				pc[i - 1] = pc[i];
				pc[i] = temp;

				Node con = new Node(pc);
				nutCon.Add(con);
				con.nutCha = this;
			}
		}
		public void DiLen(List<int> p, int i)
		{
			if (i - col >= 0)
			{
				List<int> pc = new List<int>();
				CopyPuzzle(pc, p);
				int temp = pc[i - 3];
				pc[i - 3] = pc[i];
				pc[i] = temp;

				Node con = new Node(pc);
				nutCon.Add(con);
				con.nutCha = this;
			}
		}
		public void DiXuong(List<int> p, int i)
		{
			if (i + col < mangGoc.Count)
			{
				List<int> pc = new List<int>();
				CopyPuzzle(pc, p);
				int temp = pc[i + 3];
				pc[i + 3] = pc[i];
				pc[i] = temp;

				Node con = new Node(pc);
				nutCon.Add(con);
				con.nutCha = this;
			}
		}
		public void MoRongDiChuyen()
		{
			for(int i = 0; i < mangGoc.Count; i++)
			{
				if (mangGoc[i] == 0)
					x = i;
			}
			DiQuanPhai(mangGoc, x);
			DiQuanTrai(mangGoc, x);
			DiLen(mangGoc, x);
			DiXuong(mangGoc, x);
		}
		public void InRaPuzzle()
		{
			Console.WriteLine();
			int m = 0;
			for(int i = 0; i < col; i++)
			{
				for(int j = 0; j < col; j++)
				{
					Console.WriteLine(mangGoc[m]+"");
					m++;
				}
				Console.WriteLine();
			}
		}
		public bool KiemTraTrangThaiTrung(List<int>p)
		{
			bool giongNhau = true;
			for(int i = 0; i < p.Count; i++)
			{
				if(mangGoc[i] != p[i])
				{
					giongNhau = false;
				}
			}
			return giongNhau;
		}
		public void CopyPuzzle(List<int> a, List<int> b)
		{
			for(int i = 0; i < b.Count; i++)
			{
				a[i] = b[i];
			}
		}
		public bool KiemTraDenDich()
		{
			bool denDich = true;
			int index = mangGoc[0];

			for(int i = 1; i < mangGoc.Count; i++)
			{
				if (index > mangGoc[i])
					denDich = false;
				index = mangGoc[i];
			}
			return denDich;
		}
	}

	class BestFirstSearch{
		//khoi tao
		 public BestFirstSearch()
		{

		}

		public static bool KiemTraTrangThaiChuaTrong(List<Node>list, Node check)
		{
			bool contains = false;
			for(int i = 0; i < list.Count; i++)
			{
				if (list[i].KiemTraTrangThaiTrung(check.mangGoc))
					contains = true;
			}
			return contains;
		}
		public void LanVet(List<Node>duongDi, Node n)
		{
			Console.WriteLine("lan vet:...");
			Node hienTai = n;
			duongDi.Add(hienTai);

			while(hienTai.nutCha != null)
			{
				hienTai = hienTai.nutCha;
				duongDi.Add(hienTai);
			}
		}
		public List<Node>bfs(Node goc)
		{
			List<Node> duongDiCuoiCung = new List<Node>();
			List<Node> danhSachKe = new List<Node>();
			List<Node> danhSachCacDinhDuyet = new List<Node>();

			danhSachKe.Add(goc);
			bool ketqua = false;

			while(danhSachKe.Count > 0 && !ketqua)
			{
				Node nodeHienTai = danhSachKe[0];
				danhSachCacDinhDuyet.Add(nodeHienTai);
				danhSachKe.RemoveAt(0);

				nodeHienTai.MoRongDiChuyen();
				for(int i = 0; i < nodeHienTai.nutCon.Count; i++)
				{
					Node nodeConHienTai = nodeHienTai.nutCon[i];
					if (nodeConHienTai.KiemTraDenDich())
					{
						Console.WriteLine("Chien Thang!");
						ketqua = true;
						LanVet(duongDiCuoiCung, nodeConHienTai);
					}
					if (!KiemTraTrangThaiChuaTrong(danhSachKe, nodeConHienTai) && !KiemTraTrangThaiChuaTrong(danhSachCacDinhDuyet, nodeConHienTai))
						danhSachKe.Add(nodeConHienTai);
				}
			}


			return duongDiCuoiCung;
		}
	}
}
