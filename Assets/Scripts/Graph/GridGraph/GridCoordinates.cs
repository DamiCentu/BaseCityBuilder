using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Graph
{
  public struct GridCoordinates : IEquatable<GridCoordinates>, IFormattable
  {
    private int x;
    private int y;
    private int z;
    private static readonly GridCoordinates ZERO = new GridCoordinates(0, 0, 0);
    private static readonly GridCoordinates ONE = new GridCoordinates(1, 1, 1);
    private static readonly GridCoordinates UP = new GridCoordinates(0, 1, 0);
    private static readonly GridCoordinates DOWN = new GridCoordinates(0, -1, 0);
    private static readonly GridCoordinates LEFT = new GridCoordinates(-1, 0, 0);
    private static readonly GridCoordinates RIGHT = new GridCoordinates(1, 0, 0);
    private static readonly GridCoordinates FORWARD = new GridCoordinates(0, 0, 1);
    private static readonly GridCoordinates BACK = new GridCoordinates(0, 0, -1);

    /// <summary>
    ///   <para>X component of the vector.</para>
    /// </summary>
    public int X
    {
      [MethodImpl((MethodImplOptions) 256)] get => x;
      [MethodImpl((MethodImplOptions) 256)] set => x = value;
    }

    /// <summary>
    ///   <para>Y component of the vector.</para>
    /// </summary>
    public int Y
    {
      [MethodImpl((MethodImplOptions) 256)] get => y;
      [MethodImpl((MethodImplOptions) 256)] set => y = value;
    }

    /// <summary>
    ///   <para>Z component of the vector.</para>
    /// </summary>
    public int Z
    {
      [MethodImpl((MethodImplOptions) 256)] get => z;
      [MethodImpl((MethodImplOptions) 256)] set => z = value;
    }

    /// <summary>
    ///   <para>Initializes and returns an instance of a new GridCoordinates with x and y components and sets z to zero.</para>
    /// </summary>
    /// <param name="x">The X component of the GridCoordinates.</param>
    /// <param name="y">The Y component of the GridCoordinates.</param>
    [MethodImpl((MethodImplOptions) 256)]
    public GridCoordinates(int x, int y)
    {
      this.x = x;
      this.y = y;
      z = 0;
    }

    /// <summary>
    ///   <para>Initializes and returns an instance of a new GridCoordinates with x, y, z components.</para>
    /// </summary>
    /// <param name="x">The X component of the GridCoordinates.</param>
    /// <param name="y">The Y component of the GridCoordinates.</param>
    /// <param name="z">The Z component of the GridCoordinates.</param>
    [MethodImpl((MethodImplOptions) 256)]
    public GridCoordinates(int x, int y, int z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    /// <summary>
    ///   <para>Set x, y and z components of an existing GridCoordinates.</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public void Set(int x, int y, int z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public int this[int index]
    {
      [MethodImpl((MethodImplOptions) 256)] get
      {
        switch (index)
        {
          case 0:
            return X;
          case 1:
            return Y;
          case 2:
            return Z;
          default:
            throw new IndexOutOfRangeException($"Invalid GridCoordinates index addressed: {(object) index}!");
        }
      }
      [MethodImpl((MethodImplOptions) 256)] set
      {
        switch (index)
        {
          case 0:
            X = value;
            break;
          case 1:
            Y = value;
            break;
          case 2:
            Z = value;
            break;
          default:
            throw new IndexOutOfRangeException($"Invalid GridCoordinates index addressed: {(object) index}!");
        }
      }
    }

    /// <summary>
    ///   <para>Returns the length of this vector (Read Only).</para>
    /// </summary>
    public float Magnitude { [MethodImpl((MethodImplOptions) 256)] get => Mathf.Sqrt((float) (X * X + Y * Y + Z * Z)); }

    /// <summary>
    ///   <para>Returns the squared length of this vector (Read Only).</para>
    /// </summary>
    public int SqrMagnitude { [MethodImpl((MethodImplOptions) 256)] get => X * X + Y * Y + Z * Z; }

    /// <summary>
    ///   <para>Returns the distance between a and b.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public static float Distance(GridCoordinates a, GridCoordinates b) => (a - b).Magnitude;

    /// <summary>
    ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates Min(GridCoordinates lhs, GridCoordinates rhs) => new GridCoordinates(Mathf.Min(lhs.X, rhs.X), Mathf.Min(lhs.Y, rhs.Y), Mathf.Min(lhs.Z, rhs.Z));

    /// <summary>
    ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates Max(GridCoordinates lhs, GridCoordinates rhs) => new GridCoordinates(Mathf.Max(lhs.X, rhs.X), Mathf.Max(lhs.Y, rhs.Y), Mathf.Max(lhs.Z, rhs.Z));

    /// <summary>
    ///   <para>Multiplies two vectors component-wise.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates Scale(GridCoordinates a, GridCoordinates b) => new GridCoordinates(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

    /// <summary>
    ///   <para>Multiplies every component of this vector by the same component of scale.</para>
    /// </summary>
    /// <param name="scale"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public void Scale(GridCoordinates scale)
    {
      X *= scale.X;
      Y *= scale.Y;
      Z *= scale.Z;
    }

    /// <summary>
    ///   <para>Clamps the GridCoordinates to the bounds given by min and max.</para>
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public void Clamp(GridCoordinates min, GridCoordinates max)
    {
      X = Math.Max(min.X, X);
      X = Math.Min(max.X, X);
      Y = Math.Max(min.Y, Y);
      Y = Math.Min(max.Y, Y);
      Z = Math.Max(min.Z, Z);
      Z = Math.Min(max.Z, Z);
    }

    public static Vector2Int ToVector2Int(GridCoordinates v) => new Vector2Int(v.X, v.Y);

    /// <summary>
    ///   <para>Converts a  Vector3 to a GridCoordinates by doing a Floor to each value.</para>
    /// </summary>
    /// <param name="v"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates FloorToInt(Vector3 v) => new GridCoordinates(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));

    /// <summary>
    ///   <para>Converts a  Vector3 to a GridCoordinates by doing a Ceiling to each value.</para>
    /// </summary>
    /// <param name="v"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates CeilToInt(Vector3 v) => new GridCoordinates(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));

    /// <summary>
    ///   <para>Converts a  Vector3 to a GridCoordinates by doing a Round to each value.</para>
    /// </summary>
    /// <param name="v"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates RoundToInt(Vector3 v) => new GridCoordinates(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));

    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates operator +(GridCoordinates a, GridCoordinates b) => new GridCoordinates(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates operator -(GridCoordinates a, GridCoordinates b) => new GridCoordinates(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates operator *(GridCoordinates a, GridCoordinates b) => new GridCoordinates(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates operator -(GridCoordinates a) => new GridCoordinates(-a.X, -a.Y, -a.Z);

    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates operator *(GridCoordinates a, int b) => new GridCoordinates(a.X * b, a.Y * b, a.Z * b);

    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates operator *(int a, GridCoordinates b) => new GridCoordinates(a * b.X, a * b.Y, a * b.Z);

    [MethodImpl((MethodImplOptions) 256)]
    public static GridCoordinates operator /(GridCoordinates a, int b) => new GridCoordinates(a.X / b, a.Y / b, a.Z / b);

    [MethodImpl((MethodImplOptions) 256)]
    public static bool operator ==(GridCoordinates lhs, GridCoordinates rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;

    [MethodImpl((MethodImplOptions) 256)]
    public static bool operator !=(GridCoordinates lhs, GridCoordinates rhs) => !(lhs == rhs);

    /// <summary>
    ///   <para>Returns true if the objects are equal.</para>
    /// </summary>
    /// <param name="other"></param>
    [MethodImpl((MethodImplOptions) 256)]
    public override bool Equals(object other) => other is GridCoordinates other1 && Equals(other1);

    [MethodImpl((MethodImplOptions) 256)]
    public bool Equals(GridCoordinates other) => this == other;

    /// <summary>
    ///   <para>Gets the hash code for the GridCoordinates.</para>
    /// </summary>
    /// <returns>
    ///   <para>The hash code of the GridCoordinates.</para>
    /// </returns>
    [MethodImpl((MethodImplOptions) 256)]
    public override int GetHashCode()
    {
      int hashCode1 = Y.GetHashCode();
      int hashCode2 = Z.GetHashCode();
      return X.GetHashCode() ^ hashCode1 << 4 ^ hashCode1 >> 28 ^ hashCode2 >> 4 ^ hashCode2 << 28;
    }

    /// <summary>
    ///   <para>Returns a formatted string for this vector.</para>
    /// </summary>
    /// <param name="format">A numeric format string.</param>
    /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
    public override string ToString() => ToString((string) null, (IFormatProvider) null);

    /// <summary>
    ///   <para>Returns a formatted string for this vector.</para>
    /// </summary>
    /// <param name="format">A numeric format string.</param>
    /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
    public string ToString(string format) => ToString(format, (IFormatProvider) null);

    /// <summary>
    ///   <para>Returns a formatted string for this vector.</para>
    /// </summary>
    /// <param name="format">A numeric format string.</param>
    /// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
    public string ToString(string format, IFormatProvider formatProvider)
    {
      if (formatProvider == null)
      {
        formatProvider = (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat;
      }
      return String.Format("({0}, {1}, {2})", (object) X.ToString(format, formatProvider), (object) Y.ToString(format, formatProvider), (object) Z.ToString(format, formatProvider));
    }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(0, 0, 0).</para>
    /// </summary>
    public static GridCoordinates Zero { [MethodImpl((MethodImplOptions) 256)] get => ZERO; }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(1, 1, 1).</para>
    /// </summary>
    public static GridCoordinates One { [MethodImpl((MethodImplOptions) 256)] get => ONE; }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(0, 1, 0).</para>
    /// </summary>
    public static GridCoordinates Up { [MethodImpl((MethodImplOptions) 256)] get => UP; }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(0, -1, 0).</para>
    /// </summary>
    public static GridCoordinates Down { [MethodImpl((MethodImplOptions) 256)] get => DOWN; }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(-1, 0, 0).</para>
    /// </summary>
    public static GridCoordinates Left { [MethodImpl((MethodImplOptions) 256)] get => LEFT; }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(1, 0, 0).</para>
    /// </summary>
    public static GridCoordinates Right { [MethodImpl((MethodImplOptions) 256)] get => RIGHT; }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(0, 0, 1).</para>
    /// </summary>
    public static GridCoordinates Forward { [MethodImpl((MethodImplOptions) 256)] get => FORWARD; }

    /// <summary>
    ///   <para>Shorthand for writing GridCoordinates(0, 0, -1).</para>
    /// </summary>
    public static GridCoordinates Back { [MethodImpl((MethodImplOptions) 256)] get => BACK; }
  }
}
