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
            throw new ArgumentException("��������� ��� ������� ����� ��������� ��� ����� ��� �� ������������ ������.");
        }

        // ������� ��� BFS
        Queue<(int x, int y)> queue = new();
        queue.Enqueue((currentX, currentY));

        bool[,] visited = new bool[rows, cols];
        visited[currentX, currentY] = true;

        // ������ ��� �������� ������������ ������
        (int x, int y)?[,] parent = new (int, int)?[rows, cols];

        // ����������� �������� (4 �����������: �����, ����, �����, ������)
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        // BFS
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            // ���� �������� ������� �����
            if (x == targetX && y == targetY)
            {
                bool[,] path = ReconstructPath(parent, currentX, currentY, targetX, targetY, rows, cols);
                path[currentX, currentY] = false;
                return path;
            }

            // ��������� �������� ������
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                if (IsValid(newX, newY, map) && !visited[newX, newY])
                {
                    visited[newX, newY] = true;
                    queue.Enqueue((newX, newY));
                    parent[newX, newY] = (x, y); // ���������� ��������
                }
            }
        }

        // ���� ���� �� ������
        return new bool[rows, cols]; // ���������� ������ ������
    }
    public static bool[,] FindReachable(int startX, int startY, bool[,] map, int maxMoves)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        // ������� ������ ��� ������������ ���������� ������
        bool[,] visited = new bool[rows, cols];
        visited[startX, startY] = true;

        // �������� ��������� �������
        if (!IsValid(startX, startY, visited))
        {
            throw new ArgumentException("��������� ������� ��������� ��� ����� ��� �� ������������ ������.");
        }

        // ������� ��� BFS
        Queue<(int x, int y, int moves)> queue = new();
        queue.Enqueue((startX, startY, 0));

        // ����������� �������� (4 �����������: �����, ����, �����, ������)
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        // BFS
        while (queue.Count > 0)
        {
            var (x, y, moves) = queue.Dequeue();

            // ���� �������� ������������� ���������� �����, ����������
            if (moves >= maxMoves)
                continue;

            // ��������� �������� ������
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

        // ��������������� ����, �������� �� ������� ����� � ���������
        var current = (x: targetX, y: targetY);
        while (current.x != startX || current.y != startY)
        {
            path[current.x, current.y] = true;
            current = parent[current.x, current.y].Value; // ��������� � ��������
        }

        path[startX, startY] = true; // ��������� ��������� ����� � ����
        return path;
    }
    private static bool IsValid(int x, int y, bool[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        return x >= 0 && x < rows && y >= 0 && y < cols && map[x, y];
    }
}
