﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGames.Models.Tetris
{
    public class CoordinateCollection
    {
        private readonly List<Coordinate> _coordinates = new List<Coordinate>();

        public List<Coordinate> GetAll()
        {
            return _coordinates;
        }

        public List<Coordinate> GetAllInRow(int row)
        {
            return _coordinates.Where(x => x.Row == row).ToList();
        }

        public void Add(int row, int column)
        {
            _coordinates.Add(new Coordinate(row, column));
        }

        public void AddGroup(List<Coordinate> coords, string cssClass)
        {
            foreach(var coord in coords)
            {
                _coordinates.Add(new Coordinate(coord.Row, coord.Column, cssClass));
            }
        }

        public bool Contains(int row, int column)
        {
            return _coordinates.Any(c => c.Row == row && c.Column == column);
        }

        public bool HasColumn(int column)
        {
            return _coordinates.Any(c => c.Column == column);
        }

        public List<Coordinate> GetLowest()
        {
            List<Coordinate> coords = new List<Coordinate>();
            foreach(var coord in _coordinates)
            {
                if(!Contains(coord.Row - 1, coord.Column))
                {
                    coords.Add(coord);
                }
            }

            return coords;
        }

        public List<Coordinate> GetRightmost()
        {
            List<Coordinate> coords = new List<Coordinate>();
            foreach (var coord in _coordinates)
            {
                if (!Contains(coord.Row, coord.Column + 1))
                {
                    coords.Add(coord);
                }
            }

            return coords;
        }

        public List<Coordinate> GetLeftmost()
        {
            List<Coordinate> coords = new List<Coordinate>();
            foreach (var coord in _coordinates)
            {
                if (!Contains(coord.Row, coord.Column - 1))
                {
                    coords.Add(coord);
                }
            }

            return coords;
        }

        public bool HasRow(int row)
        {
            return _coordinates.Any(c => c.Row == row);
        }

        public override string ToString()
        {
            string values = "";
            var coords = _coordinates.OrderBy(x => x.Row).ThenBy(x => x.Column);
            foreach(var coord in coords)
            {
                values += "(" + coord.Row + ", " + coord.Column + ")";
            }
            return values;
        }

        public string GetCssClass(int row, int column)
        {
            var matching = _coordinates.FirstOrDefault(x => x.Row == row && x.Column == column);

            if (matching != null)
                return matching.CssClass;

            return "";
        }

        public void SetCssClass(int row, string cssClass)
        {
            _coordinates.Where(x => x.Row == row).ToList().ForEach(x => x.CssClass = cssClass);
        }

        public void RemoveCssClass(string cssClass)
        {
            _coordinates.Where(x => x.CssClass == cssClass).ToList().ForEach(x => x.CssClass = "");
        }

        public void RemoveRows(params int[] rows)
        {
            var selectedCoords = _coordinates.Where(x => rows.Contains(x.Row)).ToList();
            foreach(var coord in selectedCoords)
            {
                _coordinates.Remove(coord);
            }
        }
    }
}