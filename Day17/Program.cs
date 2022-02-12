
// const int minX = 20, maxX = 30;
// const int minY = -5, maxY = -10;

const int minX = 207, maxX = 263;
const int minY = -63, maxY = -115;

var maxYPosition = 0;
var intercepts = 0;

for (var x = StartingX(); x <= maxX; x++)
{
    for (var y = Math.Abs(maxY) - 1; y >= maxY; y--)
    {
        if (WillHitTrench(x, y))
        {
            intercepts++;
        }
    }
}

Console.WriteLine($"Highest Y position: {maxYPosition}.");
Console.WriteLine($"Total intercepting velocities: {intercepts}");

bool WillHitTrench(int x, int y)
{
    int xV = x, xP = x;
    int yV = y, yP = y;
    
    while (xP <= maxX && yP >= maxY)
    {
        if (xP is >= minX and <= maxX && yP is <= minY and >= maxY)
        {
            return true;
        }

        if (xV != 0)
        {
            xV = xV > 0 ? xV - 1 : xV + 1;
        }
        
        --yV;
        
        xP += xV;
        yP += yV;
        
        if (yP > maxYPosition)
        {
            maxYPosition = yP;
        }
    }
    
    return false;
}

int StartingX()
{
    var x = 0;

    while (x * (x + 1) / 2 < minX)
    {
        ++x;
    }

    return x;
}
