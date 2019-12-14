using A_BFS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class frmPuzzleGame : Form
    {
        int chiSoOTrong, soBuocDi = 0;
        List<Bitmap> mangGoc = new List<Bitmap>();
		//khởi tạo phương thức đo thời gian trôi qua
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

        List<State> ketQuaCuoiCung = new List<State>();
        int currentState = 0;

        public frmPuzzleGame()
        {
            InitializeComponent();
			//khởi tạo mảng gốc để so sánh với kqua người chơi
			mangGoc.AddRange(new Bitmap[] { Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4, 
			Properties.Resources._5, Properties.Resources._6, Properties.Resources._7, Properties.Resources._8, Properties.Resources._null });
            lblBuocDi.Text += soBuocDi;
            lblThoiGianDem.Text = "00:00:00";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChoiLai();
        }

        List<int> ChoiLai()
        {
			List<int> mangRandom = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
			do
            {
                int j;
                

				mangRandom = mangRandom.OrderBy(a => Guid.NewGuid()).ToList();

                for (int i = 0; i < 9; i++)
                {
					((PictureBox)gbKhung.Controls[i]).Image = mangGoc[mangRandom[i]-1];
					if (mangRandom[i] == 9)
						chiSoOTrong = i;
				}
            } while (KiemTraWin());
			return mangRandom;
		}

        private void btnChoiLai_Click(object sender, EventArgs e)
        {
            DialogResult YesOrNo = new DialogResult();     
            if (lblThoiGianDem.Text != "00:00:00")
            {
                YesOrNo = MessageBox.Show("Bạn có muốn chơi lại hay không?","Game Ghép Hình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (YesOrNo == DialogResult.Yes || lblThoiGianDem.Text == "00:00:00")
            {
				//nếu người chơi chọn đồng ý chơi lại hoặc thời gian còn là chưa bắt đầu thì reset lại
				// tất cả các thông số như là đếm thời gian, số bước đi.
				ChoiLai();
                timer.Reset();
                lblThoiGianDem.Text = "00:00:00";
				soBuocDi = 0;
                lblBuocDi.Text = "Số Bước Đi: 0";
            }
        }

        private void KiemTraThoatChuongTrinh(object sender, FormClosingEventArgs e)
        {
            DialogResult YesOrNO = MessageBox.Show("Bạn có muốn thoát chương trình hay không ?", "Game Ghép Hình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sender as Button != btnThoat && YesOrNO == DialogResult.No) e.Cancel = true;
            if (sender as Button == btnThoat && YesOrNO == DialogResult.Yes) Environment.Exit(0);
        }

		private void btnThoat_Click(object sender, EventArgs e)
		{
			KiemTraThoatChuongTrinh(sender, e as FormClosingEventArgs);
		}

        public void renderMotO(int j, List<int> mang)
        {
            //sai chổ này
            //((PictureBox)gbKhung.Controls[j]).Image = ((PictureBox)gbKhung.Controls[j]).Image;
            ((PictureBox)gbKhung.Controls[j]).Image = mangGoc[mang[j]];
        }


        private void btnGiai_Click(object sender, EventArgs e)
        {
			//List<int> mangDau = ChoiLai();
			List<int> mangDau = new List<int> { 1, 2, 9, 3, 4, 6, 7, 5, 8 };
            List<int> mangCuoi = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            State trThaiDau = new State(mangDau);
            State trThaiCuoi = new State(mangCuoi);
            BFS bfs = new BFS(trThaiDau, trThaiCuoi);

            this.ketQuaCuoiCung = bfs.Solve();
			this.ketQuaCuoiCung.Reverse();
			this.lblBuocDi.Text = "Số Bước Đi: " + this.ketQuaCuoiCung.Count.ToString();

			this.currentState = 0;
			State tmp = this.ketQuaCuoiCung[this.currentState];
			List<int> mang = tmp.trangThai;
			for (int j = 0; j < mang.Count; j++)
			{
				((PictureBox)gbKhung.Controls[j]).Image = mangGoc[mang[j]-1];

			}
		}


		private void CachThucDiChuyen(object sender, EventArgs e)
        {
            if (lblThoiGianDem.Text == "00:00:00")
                timer.Start();
            int oNguoiDungChon = gbKhung.Controls.IndexOf(sender as Control);
            if (chiSoOTrong != oNguoiDungChon)
            {
				//tìm ra danh sách những số họ hàng với số ô người dùng chọn
                List<int> danhSachCacChiSoHoHang = new List<int>(new int[] { ((oNguoiDungChon % 3 == 0) ? -1 : oNguoiDungChon - 1), oNguoiDungChon - 3, (oNguoiDungChon % 3 == 2) ? -1 : oNguoiDungChon + 1, oNguoiDungChon + 3 });
				//nếu ô đen trống mà nằm trong ds này có nghĩa là có thể đi được.
                if (danhSachCacChiSoHoHang.Contains(chiSoOTrong))
                {
					//gán ô trống thành ô hình người dùng chọn, và gán ô người dùng chọn thành ô đen trống là ptu thứ 9 trong mảng gốc
                    ((PictureBox)gbKhung.Controls[chiSoOTrong]).Image = ((PictureBox)gbKhung.Controls[oNguoiDungChon]).Image;
                    ((PictureBox)gbKhung.Controls[oNguoiDungChon]).Image = mangGoc[8];
					chiSoOTrong = oNguoiDungChon;
                    lblBuocDi.Text = "Số Bước Đi: " + (++soBuocDi);
                    if (KiemTraWin())
                    {
                        timer.Stop();
                        (gbKhung.Controls[8] as PictureBox).Image = mangGoc[8];
                        MessageBox.Show("Chúc mừng bạn đã chiến thắng game...\nThời gian là : " + timer.Elapsed.ToString().Remove(8) + "\nSố Bước Đi : " + soBuocDi, "Game Ghép Hình");
						soBuocDi = 0;
                        lblBuocDi.Text = "Số Bước Đi: 0";
                        lblThoiGianDem.Text = "00:00:00";
                        timer.Reset();
						ChoiLai();
                    }
                }
            }
        }

        bool KiemTraWin()
        {
			//so với mảng gốc là hình ảnh gốc đã set theo thứ tự ban đầu, nếu trong quá trình duyệt có cái nào k đúng
			//thì lập tức break ra =>false, ngược lại đến 8 vẫn đúng thì trả về true
            int i;
            for (i = 0; i < 8; i++)
            {
                if ((gbKhung.Controls[i] as PictureBox).Image != mangGoc[i])
					break;
            }
            if (i == 8) return true;
            else return false;
        }

        private void TinhThoiGian(object sender, EventArgs e)
        {
			//nếu người chơi bắt đầu click vào các ô ngoại trừ ô số 8 là ô đen trống thì bắt đầu đếm thời gian
            if (timer.Elapsed.ToString() != "00:00:00")
                lblThoiGianDem.Text = timer.Elapsed.ToString().Remove(8);
            if (timer.Elapsed.ToString() == "00:00:00")
                btnTamDung.Enabled = false;
            else
                btnTamDung.Enabled = true;
			//chổ này nếu muốn giới hạn lại thời gian chơi game thì mở code ra, tùy chỉnh theo tgian mình muốn ngay chổ
			//số 1, còn ở đây em set là chơi k giới hạn thời gian

            /*if (timer.Elapsed.Minutes.ToString() == "1")
            {
                timer.Reset();
                lblBuocDi.Text = "Số Bước Đi : 0";
                lblThoiGianDem.Text = "00:00:00";
				soBuocDi = 0;
                btnTamDung.Enabled = false;
                MessageBox.Show("Hết thời gian!");
				ChoiLai();
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
			if(currentState > 0)
			{
				currentState -= 1;
				State trt = ketQuaCuoiCung[currentState];

				for (int j = 0; j < trt.trangThai.Count; j++)
				{
					((PictureBox)gbKhung.Controls[j]).Image = mangGoc[trt.trangThai[j] - 1];
				}
			}		
		}

        private void btnNext_Click(object sender, EventArgs e)
        {
			if (currentState < ketQuaCuoiCung.Count - 1)
			{
				currentState += 1;
				State trt = ketQuaCuoiCung[currentState];

				for (int j = 0; j < trt.trangThai.Count; j++)
				{
					((PictureBox)gbKhung.Controls[j]).Image = mangGoc[trt.trangThai[j] - 1];

				}
			}				
		}

		private void button2_Click_1(object sender, EventArgs e)
		{

		}

		//sự kiện click và nút tạm dừng
		private void PauseOrResume(object sender, EventArgs e)
        {
            if (btnTamDung.Text == "Tạm Dừng")
            {
                timer.Stop();
                gbKhung.Visible = false;
                btnTamDung.Text = "Tiếp Tục";
            }
            else
            {
                timer.Start();
                gbKhung.Visible = true;
                btnTamDung.Text = "Tạm Dừng";
            }
        }
    }
}
