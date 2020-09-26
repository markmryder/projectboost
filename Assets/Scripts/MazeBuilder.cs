using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BuildMaze();
    }

	private void BuildMaze()
	{
        int numberOfPaths = 3;
        int x = 20; int y = 10;
        List<List<int>> maze = new List<List<int>>();

        for (int i = 0; i < x; i++)
        {
            List<int> _list = new List<int>();
            maze.Add(_list);
            for (int j = 0; j < y; j++)
            {
                maze[i].Add(0);
            }
        }
        //maze of all 0's

        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { -1, 0, 1, 0 };

        List<List<int[]>> stack = new List<List<int[]>>();
        System.Random rand = new System.Random();

        for(int i = 0; i < numberOfPaths; i++)
		{
            int kx, ky;
			while (true)
			{
                kx = rand.Next(0, x);
                ky = rand.Next(0, y);
                if(maze[kx][ky] == 0)
				{
                    break;
				}
			}
            List<int[]> _list = new List<int[]>();
            int[] arr = { kx, ky};
            _list.Add(arr);
            //_list.Add(kx);
            //_list.Add(ky);
            stack.Add(_list);
            maze[kx][ky] = i + 1;
        }

        

        //for (int row = 0; row < x; row++)
        //{
        //    for (int col = 0; col < y; col++)
        //    {
        //        print(maze[row][col]);
        //    }

        //}

        bool cont = true;
		while (cont)
		{
            cont = false;
            for(int p = 0; p < numberOfPaths; p++)
			{
                //print(stack.Count - 1);
                if (stack[p].Count > 0)
                {
                    //print("Count of stack is: "+stack.Count);
                    cont = true;
                    int cx = stack[p][stack.Count - 1][0];
                    int cy = stack[p][stack.Count - 1][1];
                    //print(cx + " " + cy);
                    //print(cx + " " + cy);
                    List<int> neighbours = new List<int>();

                    for(int i = 0; i < 4; i++)
					{
                        int nx = cx + dx[i];
                        int ny = cy + dy[i];
                        if(nx >= 0 && nx < x && ny>=0 && ny < y)
						{
                            if(maze[nx][ny] == 0)
							{
                                int ctr = 0;
                                for(int j = 0; j < 4; j++)
								{
                                    int ex = nx + dx[j];
                                    int ey = ny + dy[j];
                                    if(ex >=0 && ex < x && ey >=0 && ey < y)
									{
                                        if(maze[ex][ey] == p + 1)
										{
                                            ctr += 1;
										}
									}
								}
                                if(ctr == 1)
								{
                                    neighbours.Add(i);
								}
							}
						}
					}
                    if(neighbours.Count > 0)
					{
                        int ir = neighbours[rand.Next(0, neighbours.Count)];
                        cx += dx[ir];
                        cy += dy[ir];
                        maze[cx][cy] = p + 1;
                        List<int[]> _list = new List<int[]>();
                        int[] arr = { cx, cy };
                        _list.Add(arr);
                        print(cx + " " + cy);
                        stack.Add(_list);
					}
					else
					{
                        stack.RemoveAt(stack.Count - 1);
					}
				}
			}
		}


		//for (int row = 0; row < x; row++)
		//{
		//	for (int col = 0; col < y; col++)
		//	{
		//		print(maze[row][col]);
		//	}

		//}


		//end of method

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
