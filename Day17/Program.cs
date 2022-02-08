// See https://aka.ms/new-console-template for more information

// const int minXPosition = 20;
// const int maxXPosition = 30;
// const int minYPosition = -10;
// const int maxYPosition = -5;

const int minXPosition = 207;
const int maxXPosition = 263;
const int minYPosition = -115;
const int maxYPosition = -63;

var targetX = Enumerable.Range(minXPosition, maxXPosition - minXPosition + 1).ToArray();
var targetY = Enumerable.Range(minYPosition, maxYPosition - minYPosition + 1).ToArray();

var xPosition = 0;
var yPosition = 0;

var xVelocity = MinX();
var yVelocity = Math.Abs(minYPosition) - 1;

var maxY = 0;

while (!InTheTrench())
{
    xPosition += xVelocity;
    yPosition += yVelocity;

    maxY = Math.Max(yPosition, maxY);

    if (xVelocity != 0)
    {
        xVelocity = xVelocity > 0 ? --xVelocity : ++xVelocity;
    }

    --yVelocity;
}

int MinX(int x = 1, int sum = 1)
{
    if (sum > targetX.Max())
    {
        throw new Exception("X out of bounds, unable to hit trench.");
    }
    
    return targetX.Contains(sum) 
        ? x 
        : MinX(++x, sum + x);
}

bool InTheTrench() => targetX.Contains(xPosition) && targetY.Contains(yPosition);

Console.WriteLine(maxY);
