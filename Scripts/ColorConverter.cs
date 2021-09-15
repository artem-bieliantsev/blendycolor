using UnityEngine;

public class ColorConverter
{
    private static readonly Vector3 D65 = new Vector3(95.047f, 100.000f, 108.883f);

    public static float[] RGBtoLab(Color color)
    {
        //conversion RGB to XYZ
        float[] RGB = { color.r, color.g, color.b };

        for (int i = 0; i < RGB.Length; i++)
        {
            if (RGB[i] > 0.04045)
                RGB[i] = Mathf.Pow((RGB[i] + 0.055f) / 1.055f, 2.4f);
            else
                RGB[i] = RGB[i] / 12.92f;

            RGB[i] *= 100;
        }

        float[] XYZ = {
            RGB[0] * 0.4124f + RGB[1] * 0.3576f + RGB[2] * 0.1805f,
            RGB[0] * 0.2126f + RGB[1] * 0.7152f + RGB[2] * 0.0722f,
            RGB[0] * 0.0193f + RGB[1]* 0.1192f + RGB[2] * 0.9505f };

        //conversion XYZ to LAB
        //Observer= 2°, Illuminant= D65
        XYZ[0] = XYZ[0] / D65.x;
        XYZ[1] = XYZ[1] / D65.y;
        XYZ[2] = XYZ[2] / D65.z;

        for (int i = 0; i < XYZ.Length; i++)
        {
            if (XYZ[i] > 0.008856f) XYZ[i] = Mathf.Pow(XYZ[i], 0.333333f);
            else XYZ[i] = (7.787f * XYZ[i]) + (16 / 116);
        }

        float[] Lab = {
            (116 * XYZ[1]) - 16,
            500 * (XYZ[0] - XYZ[1]),
            200 * (XYZ[1] - XYZ[2]) };

        return Lab;
    }
}