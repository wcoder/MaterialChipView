using Android.Content;
using Android.Graphics;
using Android.Widget;

namespace MaterialChipView
{
    internal static class ChipUtils
    {
        public static readonly int ImageId = Resource.Id.chip_image;
        public static readonly int TextId = Resource.Id.chip_text;

        public static Bitmap GetScaledBitmap(Context context, Bitmap bitmap)
        {
            int width = (int)context.Resources.GetDimension(Resource.Dimension.chip_height);
            return Bitmap.CreateScaledBitmap(bitmap, width, width, false);
        }

        public static Bitmap GetSquareBitmap(Bitmap bitmap)
        {
            Bitmap output;

            if (bitmap.Width >= bitmap.Height)
            {
                output = Bitmap.CreateBitmap(
                    bitmap,
                    bitmap.Width/ 2 - bitmap.Height / 2,
                    0,
                    bitmap.Height,
                    bitmap.Height
                );
            }
            else
            {
                output = Bitmap.CreateBitmap(
                    bitmap,
                    0,
                    bitmap.Height / 2 - bitmap.Width / 2,
                    bitmap.Width,
                    bitmap.Width
                );
            }
            return output;
        }

        public static Bitmap GetCircleBitmap(Context context, Bitmap bitmap)
        {
            int width = (int)context.Resources.GetDimension(Resource.Dimension.chip_height);
            Bitmap output = Bitmap.CreateBitmap(width, width, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(output);

            Paint paint = new Paint();
            Rect rect = new Rect(0, 0, width, width);
            RectF rectF = new RectF(rect);

            paint.AntiAlias = true;
            canvas.DrawARGB(0, 0, 0, 0);
            paint.Color = Color.Red;
            canvas.DrawOval(rectF, paint);

            paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
            canvas.DrawBitmap(bitmap, rect, rect, paint);

            return output;
        }

        public static void SetIconColor(ImageView icon, int color)
        {
            var iconDrawable = icon.Drawable;
            icon.SetColorFilter(new PorterDuffColorFilter(new Color(color), PorterDuff.Mode.SrcAtop));
            icon.SetImageDrawable(iconDrawable);
        }
    }
}