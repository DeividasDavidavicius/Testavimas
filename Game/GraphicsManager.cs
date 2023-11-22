using Models.Cells;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AbstractFactory;

namespace Game
{
    internal class GraphicsManager
    {
        public GraphicsManager() { }

        public void DrawGrid(Panel panel, Grid grid)
        {
            if (grid == null)
            {
                return;
            }

            Graphics graphics = panel.CreateGraphics();

            for (int i = 0; i < grid.gridSize; i++)
            {
                for (int j = 0; j < grid.gridSize; j++)
                {
                    Rectangle cellRect = new Rectangle(i * grid.cellSize, j * grid.cellSize, grid.cellSize, grid.cellSize);

                    Cell GridCell = grid.gridState[i, j];
                    graphics.FillRectangle(new SolidBrush(GridCell.color), cellRect);

                    // Draw cell border
                    graphics.DrawRectangle(Pens.Gray, cellRect);

                    if(GridCell is StrongOrc || GridCell is StrongDemon)
                    {
                        EnemyCell enemyCell = (EnemyCell)GridCell;

                        Font font = new Font("Arial", 12);
                        Font font2 = new Font("Arial", 8);
                        Brush textBrush = Brushes.Black;

                        string enemyCellHp = enemyCell.healthPoints.ToString();
                        string enemyClass = enemyCell.className;

                        float hpX = cellRect.Left + (cellRect.Width - graphics.MeasureString(enemyCellHp, font).Width) / 2;
                        float hpY = cellRect.Top;

                        float classX = cellRect.Left + (cellRect.Width - graphics.MeasureString(enemyClass, font2).Width) / 2;
                        float classY = cellRect.Bottom - graphics.MeasureString(enemyClass, font2).Height;

                        graphics.DrawString(enemyCellHp, font, textBrush, hpX, hpY);
                        graphics.DrawString(enemyClass, font2, textBrush, classX, classY);
                    }
                    else if (GridCell is EnemyCell)
                    {
                        EnemyCell enemyCell = (EnemyCell)GridCell;

                        Font font = new Font("Arial", 12);
                        Brush textBrush = Brushes.Black;

                        string enemyCellHp = enemyCell.healthPoints.ToString();


                        // Calculate the position for the text (centered in the cell)
                        float x = cellRect.Left + (cellRect.Width - graphics.MeasureString(enemyCellHp, font).Width) / 2;
                        float y = cellRect.Top + (cellRect.Height - graphics.MeasureString(enemyCellHp, font).Height) / 2;

                        // Draw the text
                        graphics.DrawString(enemyCellHp, font, textBrush, x, y);
                    }
                }
            }
        }
    }
}
