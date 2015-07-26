
using PathFinding.Physics;


namespace PathFinding
{
	/// <summary>
	/// Encapsulates state information about an entity.
	/// </summary>
    public class Transform
    {
	    private Point position;
	    private Directions currentDirection;


	    public Directions CurrentDirection
	    {
		    get { return this.currentDirection; }
		    set { this.currentDirection = value; }
	    }


	    public Point Position
	    {
		    get { return this.position; }
		    set { this.position = value; }
	    }

    }
}
