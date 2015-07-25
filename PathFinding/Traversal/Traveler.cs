
namespace PathFinding
{
    public class Traveler
    {
	    private Point position;
	    private Directions direction;
		private MapData mapData;
	    private ITraversalStrategy strategy;


	    public Point Position
	    {
		    get { return this.position; }
		    set { this.position = value; }
	    }


	    public MapData MapData
	    {
		    get { return this.mapData; }
		    set { this.mapData = value; }
	    }


	    public ITraversalStrategy Strategy
	    {
		    get { return this.strategy; }
		    set { this.strategy = value; }
	    }

    }
}
