using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektFeladat
{
    public partial class FormV : Form
    {
        public FormV()
        {
            InitializeComponent();
        }

        private void FormV_Load(object sender, EventArgs e)
        {

        }
		/* this is what the grid looks like
		 * grid[x][y]
		 *    0  1  2  3  x
		 * 0 [ ][ ][ ][ ]
		 * 1 [ ][ ][ ][ ]
		 * 2 [ ][ ][ ][ ]
		 * 3 [ ][ ][ ][ ]
		 * y
		 */

		//I first planned the game's size to be adjustable, but when we raise the grid size
		//the largest possible number in the game grows exponentially, and eventually the buttons will not be wide
		//enough to display those 10+ or even 100+ digit numbers.
		//But as you can see the algorithm itself uses the grid_size variable, so it is fearly easy to raise the grid size
		//
		private int grid_size;
		//list of all the buttons in the grid
		private List<Button> buttons = new List<Button>();
		private int[][] grid;
		private string pressed_key = "";

		private Dictionary<string, Color> colors = new Dictionary<string, Color>();

		int score = 0;
		//listening_for_keys is used in the keypressed() function
		//we are using this variable to only run a game cycle, when it is allowed
		bool listening_for_keys = false;
		bool game_won = false;
		bool game_lost = false;

		private void define_colors()
		{
			//defining all the colors used
			colors.Add("form_backcolor", Color.FromArgb(250, 248, 239));
			colors.Add("panel_backcolor", Color.FromArgb(187, 173, 160));
			colors.Add("tile_backcolor", Color.FromArgb(90, 238, 228, 218));
			colors.Add("tile_light_forecolor", Color.FromArgb(249, 246, 242));
			colors.Add("tile_dark_forecolor", Color.FromArgb(106, 94, 82));
			colors.Add("2", Color.FromArgb(238, 228, 218));
			colors.Add("4", Color.FromArgb(237, 224, 200));
			colors.Add("8", Color.FromArgb(242, 177, 121));
			colors.Add("16", Color.FromArgb(245, 149, 99));
			colors.Add("32", Color.FromArgb(246, 124, 95));
			colors.Add("64", Color.FromArgb(246, 94, 59));
			colors.Add("128", Color.FromArgb(237, 207, 114));
			colors.Add("256", Color.FromArgb(237, 204, 97));
			colors.Add("512", Color.FromArgb(242, 192, 61));
			colors.Add("1024", Color.FromArgb(242, 189, 51));
			colors.Add("2048", Color.FromArgb(242, 182, 49));
		}

		private void update_ui()
		{
			//going through the grid and updating their text and color
			for (int x = 0; x < grid_size; x++)
			{
				for (int y = 0; y < grid_size; y++)
				{
					if (grid[x][y] == 0)
					{
						//setting background color for empty cells
						buttons[x * grid_size + y].Text = "";
						buttons[x * grid_size + y].BackColor = colors["tile_backcolor"];
					}
					else
					{
						//setting background color for non-empty cells, using the dictionary
						buttons[x * grid_size + y].Text = Convert.ToString(grid[x][y]);
						buttons[x * grid_size + y].BackColor = colors[Convert.ToString(grid[x][y])];
					}

					//buttons with value above 4 get lighter font color
					if (grid[x][y] > 4)
					{

						buttons[x * 4 + y].ForeColor = colors["tile_light_forecolor"];
					}
					else
					{
						buttons[x * 4 + y].ForeColor = colors["tile_dark_forecolor"];
					}

					if (grid[x][y] < 16)
					{
						buttons[x * 4 + y].Font = new Font("Anonymous Pro", 30, FontStyle.Bold);
					}
					else if (grid[x][y] < 1000)
					{
						buttons[x * 4 + y].Font = new Font("Anonymous Pro", 25, FontStyle.Bold);
					}
					else
					{
						buttons[x * 4 + y].Font = new Font("Anonymous Pro", 20, FontStyle.Bold);
					}
				}
			}

			label_score.Text = Convert.ToString(score);

			//updating the label according to the standing of the game
			if (game_won)
			{
				label_gameover.Text = "GAME WON!";
			}
			else if (game_lost)
			{
				label_gameover.Text = "Game lost";
			}
			else
			{
				label_gameover.Text = "";
			}
		}

		private void add_buttons_to_list()
		{
			//this doesn't work, because it scrambles the buttons, and we need it in a specific order
			//adding every Button Control to the buttons list
			buttons.Add(button1);
			buttons.Add(button2);
			buttons.Add(button3);
			buttons.Add(button4);
			buttons.Add(button5);
			buttons.Add(button6);
			buttons.Add(button7);
			buttons.Add(button8);
			buttons.Add(button9);
			buttons.Add(button10);
			buttons.Add(button11);
			buttons.Add(button12);
			buttons.Add(button13);
			buttons.Add(button14);
			buttons.Add(button15);
			buttons.Add(button16);
		}

		private void place_tile()
		{
			//gererating a number between 0 and 10

			Random rnd = new Random();
			int num = rnd.Next(10);
			//if the number is not 4, we change it to 2
			//this way there is 90% chance of the number being 2, and 10% chance of 4
			if (num != 4)
			{
				num = 2;
			}

			bool empty_coord = false;

			int x = -1;
			int y = -1;

			//keep generating coordinates until We find a cell that equals 0
			//which means it is empty

			while (!empty_coord)
			{
				x = rnd.Next(4);
				y = rnd.Next(4);

				if (grid[x][y] == 0)
				{
					empty_coord = true;
				}
			}

			//assigning the random empty cell to the random generated number
			grid[x][y] = num;
		}

		private void generate_game()
		{

			//filling the grid with zeros, 
			//so We can easily reuse the function after a game has already been played
			for (int x = 0; x < grid_size; x++)
			{
				grid[x] = new int[grid_size];

				for (int y = 0; y < grid_size; y++)
				{
					grid[x][y] = 0;
				}
			}

			//placing 2 tiles for the game start
			place_tile();
			place_tile();
		}

		private void merge(string dir, ref int score, ref bool merge_happened)
		{
			//in the first if statement, x goes from 0 to 3, and inside that loop, y goes from 0 to 3 as well
			//if grid[x][y] is the first number in the column, we assign it as last_number
			//and in the next iteration, we check if the next number equals this
			//if they equal, we double the last number's value, and set the current number to 0
			//this way we 'merged' the two cells
			//we do this for every direction with slightly different conditions and parameters, but the principle is the same
			//and ofcourse the function reports whether a merge has happened
			if (dir == "up")
			{
				for (int x = 0; x < grid_size; x++)
				{
					int last_number = -1;

					for (int y = 0; y < grid_size; y++)
					{
						if (y == 0)
						{
							last_number = grid[x][y];
						}
						else
						{
							if (grid[x][y] != 0 && grid[x][y] == last_number)
							{
								grid[x][y - 1] *= 2;
								score += grid[x][y - 1];
								grid[x][y] = 0;
								last_number = grid[x][y];
								merge_happened = true;
							}

							last_number = grid[x][y];
						}
					}
				}
			}
			else if (dir == "down")
			{
				for (int x = 0; x < grid_size; x++)
				{
					int last_number = -1;

					for (int y = grid_size - 1; y >= 0; y--)
					{
						if (y == grid_size - 1)
						{
							last_number = grid[x][y];
						}
						else
						{
							if (grid[x][y] != 0 && grid[x][y] == last_number)
							{
								grid[x][y + 1] *= 2;
								score += grid[x][y + 1];
								grid[x][y] = 0;
								last_number = grid[x][y];
								merge_happened = true;
							}

							last_number = grid[x][y];
						}
					}
				}
			}
			else if (dir == "left")
			{
				for (int y = 0; y < grid_size; y++)
				{
					int last_number = -1;

					for (int x = 0; x < grid_size; x++)
					{
						if (x == 0)
						{
							last_number = grid[x][y];
						}
						else
						{
							if (grid[x][y] != 0 && grid[x][y] == last_number)
							{
								grid[x - 1][y] *= 2;
								score += grid[x - 1][y];
								grid[x][y] = 0;
								last_number = grid[x][y];
								merge_happened = true;
							}

							last_number = grid[x][y];
						}
					}
				}
			}
			else if (dir == "right")
			{
				for (int y = 0; y < grid_size; y++)
				{
					int last_number = -1;

					for (int x = grid_size - 1; x >= 0; x--)
					{
						if (x == grid_size)
						{
							last_number = grid[x][y];
						}
						else
						{
							if (grid[x][y] != 0 && grid[x][y] == last_number)
							{
								grid[x + 1][y] *= 2;
								score += grid[x + 1][y];
								grid[x][y] = 0;
								last_number = grid[x][y];
								merge_happened = true;
							}

							last_number = grid[x][y];
						}
					}
				}
			}
		}

		private void move(string dir, ref bool move_happened)
		{
			bool stop = false;
			while (!stop)
			{
				stop = true;
				if (dir == "up")
				{
					for (int x = 0; x < grid_size; x++)
					{
						for (int y = 0; y < grid_size; y++)
						{
							if (y != 0 && grid[x][y - 1] == 0 && grid[x][y] != 0)
							{
								grid[x][y - 1] = grid[x][y];
								grid[x][y] = 0;
								move_happened = true;
								stop = false;
							}
						}
					}
				}
				else if (dir == "down")
				{
					for (int x = 0; x < grid_size; x++)
					{
						for (int y = grid_size - 1; y >= 0; y--)
						{
							if (y != grid_size - 1 && grid[x][y + 1] == 0 && grid[x][y] != 0)
							{
								grid[x][y + 1] = grid[x][y];
								grid[x][y] = 0;
								move_happened = true;
								stop = false;
							}
						}
					}
				}
				else if (dir == "left")
				{
					for (int y = 0; y < grid_size; y++)
					{
						for (int x = 0; x < grid_size; x++)
						{
							if (x != 0 && grid[x - 1][y] == 0 && grid[x][y] != 0)
							{
								grid[x - 1][y] = grid[x][y];
								grid[x][y] = 0;
								move_happened = true;
								stop = false;
							}
						}
					}
				}
				else if (dir == "right")
				{
					for (int y = 0; y < grid_size; y++)
					{
						for (int x = grid_size - 1; x >= 0; x--)
						{
							if (x != grid_size - 1 && grid[x + 1][y] == 0 && grid[x][y] != 0)
							{
								grid[x + 1][y] = grid[x][y];
								grid[x][y] = 0;
								move_happened = true;
								stop = false;
							}
						}
					}
				}
			}
		}

		private bool winner()
		{
			for (int x = 0; x < grid_size; x++)
			{
				for (int y = 0; y < grid_size; y++)
				{
					if (grid[x][y] == 2048)
					{
						return true;
					}
				}
			}

			return false;
		}

		private void new_game()
		{
			listening_for_keys = false;
			grid_size = 4;
			grid = new int[grid_size][];
			score = 0;
			game_won = false;
			game_lost = false;

			generate_game();
			update_ui();

			listening_for_keys = true;
		}

		private void gameover()
		{
			if (game_won)
			{
				label_gameover.Text = "GAME WON!";
			}
			else if (game_lost)
			{
				label_gameover.Text = "GAME LOST";
			}
		}

		private void keypressed()
		{
			if (listening_for_keys)
			{
				string dir = pressed_key;

				//first thing is to check if there is a move available in the given direction
				//we have to store whether the move happened or not, because thats the other condiotion
				//for placing the new tile
				bool move_happened = false;
				move(dir, ref move_happened);

				//setting a tiny bit of lag so that the user can follow the game more easily
				int sleep_time = 25;

				update_ui();
				System.Threading.Thread.Sleep(sleep_time);

				//second thing is to check if a merge is available in the given direction
				//we have to store if a merge has happened, because this will be a condition for
				//placing a new tile or not
				bool merge_happened = false;
				merge(dir, ref score, ref merge_happened);

				update_ui();
				System.Threading.Thread.Sleep(sleep_time);


				//third thig is also a move
				move(dir, ref move_happened);

				update_ui();
				System.Threading.Thread.Sleep(sleep_time);

				//if something happend, we place a tile
				if (merge_happened || move_happened)
				{
					place_tile();
				}

				//checking if the user has won
				if (winner())
				{
					game_won = true;
					listening_for_keys = false;

				}

				//checking if the user has lost
				if (loser())
				{
					game_lost = true;
					listening_for_keys = false;
				}

				update_ui();
			}
		}

		private bool loser()
		{
			//if there is an empty cell we return false immediately
			for (int x = 0; x < grid_size; x++)
			{
				for (int y = 0; y < grid_size; y++)
				{
					if (grid[x][y] == 0)
					{
						return false;
					}
				}
			}

			//we now know that there are no empty cells
			//so the game can only continue, if the player can merge 2 cells
			//a merge requires 2 cell with the same value to be near each 
			//first the x axis

			for (int x = 0; x < grid_size; x++)
			{
				int last_number = -1;

				for (int y = 0; y < grid_size; y++)
				{
					if (y == 0)
					{
						//it this is the first number in the column
						//we define it as the last number for the next iteration
						last_number = grid[x][y];
					}
					else
					{
						//if this code runs, we know it is not the first number in the column
						//so we can check, if this number matches the last one

						if (grid[x][y] == last_number)
						{
							//if the 2 numbers match, this is a possible merge
							//so the game is not over
							return false;
						}

						//if the two numbers don't match
						//we mark this number as the last number for the next iteration

						last_number = grid[x][y];

					}
				}
			}

			//than the y axis

			for (int y = 0; y < grid_size; y++)
			{
				int last_number = -1;

				for (int x = 0; x < grid_size; x++)
				{
					if (x == 0)
					{
						//it this is the first number in the row
						//we define it as the last number for the next iteration
						last_number = grid[x][y];
					}
					else
					{
						//if this code runs, we know it is not the first number in the row
						//so we can check, if this number matches the last one

						if (grid[x][y] == last_number)
						{
							//if the 2 numbers match, this is a possible merge
							//so the game is not over
							return false;
						}

						//if the two numbers don't match
						//we mark this number as the last number for the next iteration

						last_number = grid[x][y];

					}
				}
			}

			return true;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			listening_for_keys = false;
			define_colors();
			this.BackColor = colors["form_backcolor"];
			panel1.BackColor = colors["panel_backcolor"];
			panel2.BackColor = colors["panel_backcolor"];
			panel3.BackColor = colors["panel_backcolor"];
			label1.ForeColor = colors["tile_backcolor"];
			label7.ForeColor = colors["tile_backcolor"];
			label_score.ForeColor = colors["form_backcolor"];
			label_best_score.ForeColor = colors["form_backcolor"];
			button_newgame.BackColor = colors["panel_backcolor"];
			button_exit.BackColor = colors["panel_backcolor"];
			button_exit.ForeColor = colors["form_backcolor"];
			button_newgame.ForeColor = colors["form_backcolor"];

			add_buttons_to_list();
			new_game();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{

		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e)
		{

			if (e.KeyChar == 's')
			{
				pressed_key = "down";
			}
			else if (e.KeyChar == 'w')
			{
				pressed_key = "up";
			}
			else if (e.KeyChar == 'a')
			{
				pressed_key = "left";
			}
			else if (e.KeyChar == 'd')
			{
				pressed_key = "right";
			}

			keypressed();
		}

		private void button1_KeyPress(object sender, KeyPressEventArgs e)
		{

		}

		private void button18_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button_newgame_Click(object sender, EventArgs e)
		{
			new_game();
		}

		private void label_gamover_Click(object sender, EventArgs e)
		{

		}

		private void label7_Click(object sender, EventArgs e)
		{

		}
	}
}
