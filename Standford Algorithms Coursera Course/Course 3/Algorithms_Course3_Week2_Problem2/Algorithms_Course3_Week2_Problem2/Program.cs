using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace Algorithms_Course3_Week2_Problem2
{
    class Program
    {
        static void Main(string[] args)
        {
            // string path = @"\clustering_big.txt";
            string path = Directory.GetCurrentDirectory() + @"\clustering_big.txt";
            string[] lines = File.ReadAllLines(path);
            NodeValues[] _nodeValues = new NodeValues[lines.Length + 1];
            int index = 0;
            int[] _parentArray = new int[_nodeValues.Length + 1];
            Hashtable _groupSizes = new Hashtable();
            uint[] bitMasksHammingDistance_1 = new uint[24];
            uint[] bitMasksHammingDistance_2 = new uint[276];
            int _noOfNodes = _nodeValues.Length - 1;
            foreach (var s in lines)
            {
                index++;
                int pow = 23;
                string[] bits = s.TrimEnd(' ').Split(' ');
                _nodeValues[index] = new NodeValues();
                foreach (var b in bits)
                {
                    _nodeValues[index].node = index;
                    _nodeValues[index].value += (uint)(Convert.ToInt16(b) * Math.Pow(2, pow));
                    pow--;
                }
            }


            //filling the values in the bitmasks for hamming distance 1
            for (int i = 0; i < 24; i++)
            {
                bitMasksHammingDistance_1[i] = (uint)Math.Pow(2, i);
            }

            index = 0;

            //filling the values in the bitmasks for hamming distance 2
            for (int i = 0; i < 23; i++)
            {
                for (int j = i + 1; j < 24; j++)
                {
                    bitMasksHammingDistance_2[index] = (uint)(Math.Pow(2, i) + Math.Pow(2, j));
                    index++;
                }

            }


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            QuickSort(1, _nodeValues.Length - 1);


            uint _max = _nodeValues[_nodeValues.Length - 1].value;

            for (int i = 1; i < _nodeValues.Length; i++)
            {
                int node1 = _nodeValues[i].node;
                uint _value1 = _nodeValues[i].value;
                uint _valueToFind = _value1;
                index = i + 1;
                int _leader1;
                int _leader2;
                while (index < _nodeValues.Length && _nodeValues[index].value == _valueToFind)
                {
                    _leader1 = FindLeader(node1);
                    _leader2 = FindLeader(_nodeValues[index].node);
                    if (_leader1 != _leader2)
                    {
                        Union(_leader1, _leader2);
                        _noOfNodes--;
                    }
                    index++;
                }

                for (int j = 0; j < 24; j++)
                {
                    _valueToFind = _value1 ^ bitMasksHammingDistance_1[j];
                    if (_valueToFind <= _max)
                    {
                        int node2 = FindValue(1, _nodeValues.Length - 1, _valueToFind);

                        if (node2 != 0)
                        {

                            _leader1 = FindLeader(node1);
                            _leader2 = FindLeader(node2);

                            if (_leader1 != _leader2)
                            {
                                Union(_leader1, _leader2);
                                _noOfNodes--;
                            }
                        }
                    }
                }

                for (int j = 0; j < 276; j++)
                {
                    _valueToFind = _value1 ^ bitMasksHammingDistance_2[j];
                    if (_valueToFind <= _max)
                    {
                        int node2 = FindValue(1, _nodeValues.Length - 1, _valueToFind);

                        if (node2 != 0)
                        {

                            _leader1 = FindLeader(node1);
                            _leader2 = FindLeader(node2);

                            if (_leader1 != _leader2)
                            {
                                Union(_leader1, _leader2);
                                _noOfNodes--;
                            }
                        }
                    }
                }

            }

            Console.WriteLine("ans is {0}", _noOfNodes);
            Console.WriteLine("time elapsed is {0}", stopwatch.ElapsedMilliseconds);

            void Union(int _lead1, int _lead2)
            {
                int _grpSize1 = _groupSizes.ContainsKey(_lead1) ? (int)_groupSizes[_lead1] : 1;
                int _grpSize2 = _groupSizes.ContainsKey(_lead2) ? (int)_groupSizes[_lead2] : 1;



                if (_grpSize1 > _grpSize2)
                {
                    _parentArray[_lead2] = _lead1;
                    _groupSizes[_lead1] = _grpSize1 + _grpSize2;
                }
                else
                {
                    if (_grpSize1 == 1 && _grpSize2 == 1)
                    {
                        _groupSizes.Add(_lead2, 2);
                        _parentArray[_lead1] = _lead2;
                    }
                    else
                    {
                        _parentArray[_lead1] = _lead2;
                        _groupSizes[_lead2] = _grpSize2 + _grpSize1;
                    }
                }
            }

            int FindLeader(int node)
            {
                while (_parentArray[node] != 0)
                {
                    node = _parentArray[node];
                }
                return node;
            }

            int FindValue(int start_pos, int last_pos, uint _value)
            {
                int len = last_pos - start_pos + 1;
                if (len > 1)
                {
                    int mid = start_pos + (len / 2);
                    if (_value < _nodeValues[mid].value)
                    {
                        return FindValue(start_pos, mid - 1, _value);

                    }
                    else if (_value > _nodeValues[mid].value)
                    {
                        return FindValue(mid + 1, last_pos, _value);
                    }
                    else
                    {
                        return _nodeValues[mid].node;
                    }
                }
                else
                {
                    if (_nodeValues[start_pos].value == _value)
                    {
                        return _nodeValues[start_pos].node;
                    }
                    else
                    {
                        return 0;
                    }

                }
            }

            void QuickSort(int start_pos, int last_pos)
            {
                int len = last_pos - start_pos + 1;
                if (len > 1)
                {
                    int i = start_pos;

                    uint pivot = _nodeValues[start_pos].value;

                    for (int j = start_pos + 1; j <= last_pos; j++)
                    {
                        if (_nodeValues[j].value < pivot)
                        {
                            Swap(i + 1, j);
                            i++;
                        }

                    }
                    Swap(i, start_pos);
                    QuickSort(start_pos, i - 1);
                    QuickSort(i + 1, last_pos);
                }
            }

            void Swap(int i, int j)
            {
                NodeValues temp = _nodeValues[i];
                _nodeValues[i] = _nodeValues[j];
                _nodeValues[j] = temp;
            }
            Console.ReadKey();
        }
    }
}
