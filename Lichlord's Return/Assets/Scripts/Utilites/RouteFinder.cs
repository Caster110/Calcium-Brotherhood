using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class RouteFinder
{
    public static bool[,] FindRoute(int targetX, int targetY, int currentX, int currentY, Grid<CombatEntity> map)
    {
        bool[,] convertedMap = new bool[map.Width, map.Height];
        for (int x = 0; x < map.Width;)
        {
            for (int y = 0; y < map.Height;)
            {
                convertedMap[x, y] = map.GetGridObjectValue(x, y).IsPassable;
            }
        }
        return FindRoute(targetX, targetY, currentX, currentY, convertedMap);
    }
    public static bool[,] FindRoute(int targetX, int targetY, int currentX, int currentY, bool[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        if (!IsValid(targetX, targetY, map) || !IsValid(currentX, currentY, map))
        {
            throw new ArgumentException("Начальная или целевая точка находится вне карты или на непроходимой клетке.");
        }

        // Очередь для BFS
        Queue<(int x, int y)> queue = new();
        queue.Enqueue((currentX, currentY));

        bool[,] visited = new bool[rows, cols];
        visited[currentX, currentY] = true;

        // Массив для хранения родительских клеток
        (int x, int y)?[,] parent = new (int, int)?[rows, cols];

        // Направления движения (4 направления: вверх, вниз, влево, вправо)
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        // BFS
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            // Если достигли целевой точки
            if (x == targetX && y == targetY)
            {
                bool[,] path = ReconstructPath(parent, currentX, currentY, targetX, targetY, rows, cols);
                path[currentX, currentY] = false;
                return path;
            }

            // Проверяем соседние клетки
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                if (IsValid(newX, newY, map) && !visited[newX, newY])
                {
                    visited[newX, newY] = true;
                    queue.Enqueue((newX, newY));
                    parent[newX, newY] = (x, y); // Запоминаем родителя
                }
            }
        }

        // Если путь не найден
        return new bool[rows, cols]; // Возвращаем пустой массив
    }
    public static bool[,] FindReachable(int startX, int startY, bool[,] map, int maxMoves)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        // Создаем массив для отслеживания посещенных клеток
        bool[,] visited = new bool[rows, cols];
        visited[startX, startY] = true;

        // Проверка граничных условий
        if (!IsValid(startX, startY, visited))
        {
            throw new ArgumentException("Начальная позиция находится вне карты или на непроходимой клетке.");
        }

        // Очередь для BFS
        Queue<(int x, int y, int moves)> queue = new();
        queue.Enqueue((startX, startY, 0));

        // Направления движения (4 направления: вверх, вниз, влево, вправо)
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        // BFS
        while (queue.Count > 0)
        {
            var (x, y, moves) = queue.Dequeue();

            // Если достигли максимального количества шагов, продолжаем
            if (moves >= maxMoves)
                continue;

            // Проверяем соседние клетки
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                if (IsValid(newX, newY, map) && !visited[newX, newY])
                {
                    visited[newX, newY] = true;
                    queue.Enqueue((newX, newY, moves + 1));
                }
            }
        }
        visited[startX, startY] = false;
        return visited;
    }
    private static bool[,] ReconstructPath((int x, int y)?[,] parent, int startX, int startY, int targetX, int targetY, int rows, int cols)
    {
        bool[,] path = new bool[rows, cols];

        // Восстанавливаем путь, двигаясь от целевой точки к начальной
        var current = (x: targetX, y: targetY);
        while (current.x != startX || current.y != startY)
        {
            path[current.x, current.y] = true;
            current = parent[current.x, current.y].Value; // Переходим к родителю
        }

        path[startX, startY] = true; // Добавляем начальную точку в путь
        return path;
    }
    private static bool IsValid(int x, int y, bool[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        return x >= 0 && x < rows && y >= 0 && y < cols && map[x, y];
    }
}
