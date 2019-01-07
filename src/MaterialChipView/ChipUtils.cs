using Android.Content;
using Android.Graphics;
using Android.Widget;
using System;

namespace MaterialChipView
{
    internal static class ChipUtils
    {
        public static readonly int ImageId = Resource.Id.chip_image;
        public static readonly int TextId = Resource.Id.chip_text;

        private static int[] Colors = {0xd32f2f, 0xC2185B, 0x7B1FA2, 0x512DA8, 0x303F9F, 0x1976D2, 0x0288D1, 0x0097A7, 0x00796B, 0x388E3C, 0x689F38,
            0xAFB42B, 0xFBC02D, 0xFFA000, 0xF57C00, 0xE64A19, 0x5D4037, 0x616161, 0x455A64};

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

        public static Bitmap getCircleBitmapWithText(Context context, string text, int bgColor, int textColor)
        {
            int width = (int)context.Resources.GetDimension(Resource.Dimension.chip_height);
            var output = Bitmap.CreateBitmap(width, width, Bitmap.Config.Argb8888);
            var canvas = new Canvas(output);

            var paint = new Paint();
            var textPaint = new Paint();
            var rect = new Rect(0, 0, width, width);
            var rectF = new RectF(rect);

            paint.AntiAlias = true;
            canvas.DrawARGB(0, 0, 0, 0);
            paint.Color = new Color(bgColor);
            canvas.DrawOval(rectF, paint);
            textPaint.Color = new Color(textColor);
            textPaint.StrokeWidth = 30;
            textPaint.TextSize = 50;
            paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcOver));
            textPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcAtop));

            int xPos;
            int yPos;
            if (text.Length == 1)
            {
                xPos = (int)((canvas.Width / 1.9f) + ((textPaint.Descent() + textPaint.Ascent()) / 2f));
                yPos = (int)((canvas.Height / 2f) - ((textPaint.Descent() + textPaint.Ascent()) / 2f));
            }
            else
            {
                xPos = (int)((canvas.Width / 2.7f) + ((textPaint.Descent() + textPaint.Ascent()) / 2f));
                yPos = (int)((canvas.Height / 2f) - ((textPaint.Descent() + textPaint.Ascent()) / 2f));
            }

            canvas.DrawBitmap(output, rect, rect, paint);
            canvas.DrawText(text, xPos, yPos, textPaint);

            return output;
        }

        public static string GenerateText(string iconText)
        {
            if (string.IsNullOrEmpty(iconText))
            {
                throw new ArgumentNullException("Icon text must have at least one symbol");
            }
            if (iconText.Length == 1 || iconText.Length == 2)
            {
                return iconText;
            }

            var parts = iconText.Split(" ");
            if (parts.Length == 1)
            {
                var text = parts[0];
                text = text.Substring(0, 2);

                var f = text.Substring(0, 1);
                var s = text.Substring(1, 2);

                f = f.ToUpper();
                s = s.ToLower();

                text = string.Concat(f, s);

                return text;
            }
            var first = parts[0];
            var second = parts[1];

            first = first.Substring(0, 1);
            first = first.ToUpper();
            second = second.Substring(0, 1);
            second = second.ToUpper();

            return string.Concat(first, second);
        }

        public static void SetIconColor(ImageView icon, int color)
        {
            var iconDrawable = icon.Drawable;
            icon.SetColorFilter(new PorterDuffColorFilter(new Color(color), PorterDuff.Mode.SrcAtop));
            icon.SetImageDrawable(iconDrawable);
        }
    }
}