

using System.Drawing;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// Defines the drawing constraint that all graph types must implement.
	/// </summary>
	interface IGraphElement
	{
		void Draw (Graphics graphics);
	}
}
