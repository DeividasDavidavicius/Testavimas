using Models;
using Models.Bridge;
using Models.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class WeaponTests
    {
        [TestMethod]
        public void StrongWeapon_SpawnsSafe()
        {
            // Arrange
            GameSession session = new GameSession();
            Cell[,] gridState = new Cell[5, 5];
            Grid grid = new Grid(5, 5, gridState, 2);
            IWeaponSpawner weaponSpawner;
            weaponSpawner = new SafeWeaponSpawner();
            Weapon weapon = new StrongWeapon(weaponSpawner, grid, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    gridState[i, j] = new EmptyCell();
                }
            }

            // Act
            weapon.SpawnWeapon();

            // Assert
            bool weaponFound = false;
            WeaponCell weaponCell = new WeaponCell(Color.AliceBlue, 0, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(gridState[i, j].GetType() == typeof(WeaponCell))
                    {
                        weaponFound = true;
                        weaponCell = (WeaponCell)gridState[i, j];
                    }
                }
            }

            Assert.IsTrue(weaponFound);
            Assert.IsTrue(weaponCell.color == Color.Gray && weaponCell.damagePoints == 10);
        }

        [TestMethod]
        public void StrongWeapon_SpawnsDangerous()
        {
            // Arrange
            GameSession session = new GameSession();
            Cell[,] gridState = new Cell[5, 5];
            Grid grid = new Grid(5, 5, gridState, 2);
            IWeaponSpawner weaponSpawner;
            weaponSpawner = new DangerousWeaponSpawner();
            Weapon weapon = new StrongWeapon(weaponSpawner, grid, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    gridState[i, j] = new EmptyCell();
                }
            }

            gridState[4, 4] = new EnemyCell();

            // Act
            weapon.SpawnWeapon();

            // Assert
            bool weaponFound = false;
            WeaponCell weaponCell = new WeaponCell(Color.AliceBlue, 0, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (gridState[i, j].GetType() == typeof(WeaponCell))
                    {
                        weaponFound = true;
                        weaponCell = (WeaponCell)gridState[i, j];
                    }
                }
            }

            Assert.IsTrue(weaponFound);
            Assert.IsTrue(weaponCell.color == Color.Gray && weaponCell.damagePoints == 10);
        }

        [TestMethod]
        public void WeakWeapon_SpawnsSafe()
        {
            // Arrange
            GameSession session = new GameSession();
            Cell[,] gridState = new Cell[5, 5];
            Grid grid = new Grid(5, 5, gridState, 2);
            IWeaponSpawner weaponSpawner;
            weaponSpawner = new SafeWeaponSpawner();
            Weapon weapon = new WeakWeapon(weaponSpawner, grid, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    gridState[i, j] = new EmptyCell();
                }
            }

            // Act
            weapon.SpawnWeapon();

            // Assert
            bool weaponFound = false;
            WeaponCell weaponCell = new WeaponCell(Color.AliceBlue, 0, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (gridState[i, j].GetType() == typeof(WeaponCell))
                    {
                        weaponFound = true;
                        weaponCell = (WeaponCell)gridState[i, j];
                    }
                }
            }

            Assert.IsTrue(weaponFound);
            Assert.IsTrue(weaponCell.color == Color.LightGray && weaponCell.damagePoints == 5);
        }

        [TestMethod]
        public void WeakWeapon_SpawnsDangerous()
        {
            // Arrange
            GameSession session = new GameSession();
            Cell[,] gridState = new Cell[5, 5];
            Grid grid = new Grid(5, 5, gridState, 2);
            IWeaponSpawner weaponSpawner;
            weaponSpawner = new DangerousWeaponSpawner();
            Weapon weapon = new WeakWeapon(weaponSpawner, grid, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    gridState[i, j] = new EmptyCell();
                }
            }

            gridState[4, 4] = new EnemyCell();

            // Act
            weapon.SpawnWeapon();

            // Assert
            bool weaponFound = false;
            WeaponCell weaponCell = new WeaponCell(Color.AliceBlue, 0, session);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (gridState[i, j].GetType() == typeof(WeaponCell))
                    {
                        weaponFound = true;
                        weaponCell = (WeaponCell)gridState[i, j];
                    }
                }
            }

            Assert.IsTrue(weaponFound);
            Assert.IsTrue(weaponCell.color == Color.LightGray && weaponCell.damagePoints == 5);
        }
    }
}
