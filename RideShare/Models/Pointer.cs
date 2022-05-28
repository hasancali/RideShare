using RideShare.ENums;

namespace RideShare.Models
{
    public class Pointer : Trip2
    {
        public List<int> RouteCity { get; set; } = new List<int>();

        public void CreateRoute()
        {
            int k = 0;

            int direction = 0;

            var modFrom = From % 20;
            var modTo = To % 20;

            if (modFrom > modTo)
                direction += (int)Directions.Left;
            else if (modFrom < modTo)
                direction += (int)Directions.Right;


            var heightFrom = From / 20;
            var heightTo = To / 20;

            if (heightFrom > heightTo)
                direction += (int)Directions.Up;
            else if (heightFrom < heightTo)
                direction += (int)Directions.Down;

            var sideList = new HashSet<int>();

            switch (direction)
            {
                case (int)Directions.Up:

                    for (int i = From; i >= To; i = i - 20)
                    {
                        sideList.Add(i);

                        var iMod = i % 20;
                        if (iMod - 1 >= 0)
                            sideList.Add(i - 1);
                        if (iMod + 1 <= 19)
                            sideList.Add(i + 1);
                    }
                    break;

                case (int)Directions.Down:
                    for (int i = From; i <= To; i = i + 20)
                    {
                        sideList.Add(i);

                        var iMod = i % 20;
                        if (iMod - 1 >= 0)
                            sideList.Add(i - 1);
                        if (iMod + 1 <= 19)
                            sideList.Add(i + 1);
                    }
                    break;

                case (int)Directions.Left:
                    for (int i = From; i >= To; i--)
                    {
                        sideList.Add(i);

                        if (i - 20 >= 0)
                            sideList.Add(i - 20);
                        if (i + 20 <= 199)
                            sideList.Add(i + 20);
                    }
                    break;
                case (int)Directions.Right:
                    for (int i = From; i <= To; i++)
                    {
                        sideList.Add(i);

                        if (i - 20 >= 0)
                            sideList.Add(i - 20);
                        if (i + 20 <= 199)
                            sideList.Add(i + 20);
                    }
                    break;
                case (int)Directions.UpRight:
                    for (k = From; k >= To - modTo; k = k - 19)
                    {
                        sideList.Add(k);
                        var kMod = k % 20;
                        if (kMod - 1 >= 0)
                            sideList.Add(k - 1);
                        if (kMod + 1 <= 19)
                            sideList.Add(k + 1);
                    }

                    for (int j = k + 19; j <= To; j++)
                    {
                        sideList.Add(j);
                        if (j - 20 >= 0)
                            sideList.Add(j - 20);
                        if (j + 20 <= 199)
                            sideList.Add(j + 20);
                    }
                    break;
                case (int)Directions.UpLeft:
                    for (k = From; k >= To - modTo; k = k - 21)
                    {
                        sideList.Add(k);
                        var iMod = k % 20;
                        if (iMod - 1 >= 0)
                            sideList.Add(k - 1);
                        if (iMod + 1 <= 19)
                            sideList.Add(k + 1);
                    }

                    for (int j = k + 21; j >= To; j--)
                    {
                        sideList.Add(j);
                        if (j - 20 >= 0)
                            sideList.Add(j - 20);
                        if (j + 20 <= 199)
                            sideList.Add(j + 20);
                    }
                    break;
                case (int)Directions.DownRight:
                    for (k = From; k < To; k = k + 21)
                    {
                        sideList.Add(k);

                        if (k - 20 >= 0)
                            sideList.Add(k - 20);
                        if (k + 20 <= 199)
                            sideList.Add(k + 20);
                    }

                    for (int j = k - 21; j <= To; j++)
                    {
                        sideList.Add(j);
                        if (j - 1 >= 0)
                            sideList.Add(j - 1);
                        if (j + 1 <= 19)
                            sideList.Add(j + 1);
                    }
                    break;
                case (int)Directions.DownLeft:

                    for (k = From; k < To; k = k + 19)
                    {
                        sideList.Add(k);

                        if (k - 20 >= 0)
                            sideList.Add(k - 20);
                        if (k + 20 <= 199)
                            sideList.Add(k + 20);
                    }

                    for (int j = k; j >= To; j--)
                    {
                        sideList.Add(j);
                        if (j - 1 >= 0)
                            sideList.Add(j - 1);
                        if (j + 1 <= 19)
                            sideList.Add(j + 1);
                    }
                    break;
                default:
                    break;
            }

            RouteCity = sideList.ToList();
        }
    }
}

