using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Content;

using static MaterialChipView.ChipUtils;

namespace MaterialChipView
{
	public class Chip : RelativeLayout
	{
        #region fields

        private string _chipText;
        private bool _hasIcon;
        private Drawable _chipIcon;
        private bool _closable;
        private bool _selectable;
        private int _backgroundColor;
        private int _selectedBackgroundColor;
        private int _textColor;
        private int _selectedTextColor;
        private int _closeColor;
        private int _selectedCloseColor;

        private ImageView _closeIcon;
        private ImageView _selectIcon;

        private bool _clicked;
        private bool _selected;

        #endregion

        #region events

        public event EventHandler Close;
        public event EventHandler IconClick;
	    public event EventHandler Select;

        #endregion

        #region props

        /// <summary>
        /// Chip label text
        /// </summary>
        public string ChipText
	    {
	        get => _chipText;
	        set => _chipText = value;
	    }

        /// <summary>
        /// Chip label color
        /// </summary>
	    public int TextColor
	    {
	        get => _textColor;
	        set => _textColor = value;
	    }

        /// <summary>
        /// Custom background color
        /// </summary>
	    public int BackgroundColor
	    {
	        get => _backgroundColor;
	        set => _backgroundColor = value;
	    }

        /// <summary>
        /// Custom background color when selected
        /// </summary>
	    public int SelectedBackgroundColor
	    {
	        get => _selectedBackgroundColor;
	        set => _selectedBackgroundColor = value;
	    }

        /// <summary>
        /// Chip has icon
        /// </summary>
        public bool HasIcon
        {
            get => _hasIcon;
            set => _hasIcon = value;
        }

        /// <summary>
        /// Icon Drawable for Chip
        /// </summary>
        public Drawable ChipIcon
	    {
	        get => _chipIcon;
	        set => _chipIcon = value;
	    }

        /// <summary>
        /// Chip has close button
        /// </summary>
	    public bool Closable
	    {
	        get => _closable;
            set
            {
	            _closable = value;
	            _selectable = false;
            }
	    }

        /// <summary>
        /// Custom color for close button
        /// </summary>
        public int CloseColor
	    {
	        get => _closeColor;
	        set => _closeColor = value;
	    }

	    /// <summary>
        /// Chip has selection button
        /// </summary>
        public bool Selectable
        {
            get => _selectable;
            set
            {
                _selectable = value;
                _closable = false;
            }
        }

	    /// <summary>
	    /// Chip as clicked
	    /// </summary>
	    public bool Clicked
	    {
	        set => _clicked = value;
	    }

	    /// <summary>
	    /// Custom color for label when selected
	    /// </summary>
	    public int SelectedTextColor
	    {
	        get => _selectedTextColor;
	        set => _selectedTextColor = value;
	    }

        /// <summary>
        /// Custom color for close button when selected
        /// </summary>
	    public int SelectedCloseColor
	    {
	        get => _selectedCloseColor;
	        set => _selectedCloseColor = value;
	    }

        #endregion

        #region ctors

        public Chip(Context context)
            : this(context, null, 0)
        {
        }

        public Chip(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public Chip(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            InitTypedArray(attrs);
        }

        #endregion

        #region override

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                BuildView();
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            var thisParams = LayoutParameters;

            thisParams.Width = ViewGroup.LayoutParams.WrapContent;
            thisParams.Height = (int) Resources.GetDimension(Resource.Dimension.chip_height);

            LayoutParameters = thisParams;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            if (Build.VERSION.SdkInt < BuildVersionCodes.N)
            {
                BuildView();
            }
        }

        #endregion

        #region private methods

        private void BuildView()
        {
            InitBackgroundColor();
            InitTextView();
            InitImageIcon();
            InitCloseIcon();
            InitSelectIcon();
        }

        private void InitSelectClick()
        {
            _selectIcon.Touch += (sender, args) =>
            {
                switch (args.Event.Action) {
                    case MotionEventActions.Down:
                    case MotionEventActions.Pointer1Down:
                        OnSelectTouchDown();
                        break;
                    case MotionEventActions.Up:
                    case MotionEventActions.Pointer1Up:
                        OnSelectTouchUp((View)sender);
                        break;
                }
            };
        }

        private void OnSelectTouchDown()
        {
            _clicked = !_clicked;
            InitBackgroundColor();
            InitTextView();
            _selectIcon.SetImageResource(Resource.Drawable.ic_select);
            SetIconColor(_selectIcon, _clicked ? _selectedCloseColor : _closeColor);
        }

        private void OnSelectTouchUp(View v)
        {
            _selected = !_clicked;
            if (_selected)
            {
                _clicked = false;
                InitBackgroundColor();

                InitTextView();
                _selectIcon.SetImageResource(Resource.Drawable.ic_select);
                SetIconColor(_selectIcon, _closeColor);
            }
            _selected = !_selected;

            OnSelectClick(v, _selected);
        }

        private void InitCloseClick()
        {
            _closeIcon.Touch += (sender, args) =>
            {
                switch (args.Event.Action) {
                    case MotionEventActions.Down:
                    case MotionEventActions.Pointer1Down:
                        OnCloseTouchDown();
                        break;
                    case MotionEventActions.Up:
                    case MotionEventActions.Pointer1Up:
                        OnCloseTouchUp((View)sender);
                        break;
                }
            };
        }

        private void OnCloseTouchDown()
        {
            _clicked = true;
            InitBackgroundColor();
            InitTextView();
            _closeIcon.SetImageResource(Resource.Drawable.ic_close);
            SetIconColor(_closeIcon, _selectedCloseColor);
        }

        private void OnCloseTouchUp(View v)
        {
            _clicked = false;
            InitBackgroundColor();
            InitTextView();
            _closeIcon.SetImageResource(Resource.Drawable.ic_close);
            SetIconColor(_closeIcon, _closeColor);

            OnCloseClick(v);
        }

        private void InitSelectIcon()
        {
            if (!_selectable) return;

            _selectIcon = new ImageView(Context);

            var selectIconParams = new LayoutParams((int)Resources.GetDimension(Resource.Dimension.chip_close_icon_size2), (int)Resources.GetDimension(Resource.Dimension.chip_close_icon_size2));
            selectIconParams.AddRule(Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1 ? LayoutRules.EndOf : LayoutRules.RightOf, TextId);
            selectIconParams.AddRule(LayoutRules.CenterVertical);
            selectIconParams.SetMargins(
                (int)Resources.GetDimension(Resource.Dimension.chip_close_horizontal_margin),
                0,
                (int)Resources.GetDimension(Resource.Dimension.chip_close_horizontal_margin),
                0
            );

            _selectIcon.LayoutParameters = selectIconParams;
            _selectIcon.SetScaleType(ImageView.ScaleType.Center);
            _selectIcon.SetImageResource(Resource.Drawable.ic_select);
            SetIconColor(_selectIcon, _clicked ? _selectedCloseColor : _closeColor);

            InitSelectClick();

            AddView(_selectIcon);
        }

        private void InitCloseIcon()
        {
            if (!_closable) return;

            _closeIcon = new ImageView(Context);

            var closeIconParams = new LayoutParams((int)Resources.GetDimension(Resource.Dimension.chip_close_icon_size2), (int)Resources.GetDimension(Resource.Dimension.chip_close_icon_size2));
            closeIconParams.AddRule(Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1 ? LayoutRules.EndOf : LayoutRules.RightOf, TextId);
            closeIconParams.AddRule(LayoutRules.CenterVertical);
            closeIconParams.SetMargins(
                (int)Resources.GetDimension(Resource.Dimension.chip_close_horizontal_margin),
                0,
                (int)Resources.GetDimension(Resource.Dimension.chip_close_horizontal_margin),
                0
            );

            _closeIcon.LayoutParameters = closeIconParams;
            _closeIcon.SetScaleType(ImageView.ScaleType.Center);
            _closeIcon.SetImageResource(Resource.Drawable.ic_close);
            SetIconColor(_closeIcon, _closeColor);

            InitCloseClick();

            AddView(_closeIcon);
        }

        private void InitImageIcon()
        {
            if (!_hasIcon) return;

            var icon = new ImageView(Context);
            var iconParams = new LayoutParams((int)Resources.GetDimension(Resource.Dimension.chip_height), (int)Resources.GetDimension(Resource.Dimension.chip_height));
            iconParams.AddRule(Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1 ? LayoutRules.AlignParentStart : LayoutRules.AlignParentLeft);
            icon.LayoutParameters = iconParams;
            icon.SetScaleType(ImageView.ScaleType.FitCenter);
            icon.Id = ImageId;

            var bitmap = ((BitmapDrawable) _chipIcon).Bitmap;
            if (_chipIcon != null && bitmap != null)
            {
                bitmap = GetSquareBitmap(bitmap);
                bitmap = GetScaledBitmap(Context, bitmap);
                icon.SetImageBitmap(GetCircleBitmap(Context, bitmap));
            }

            icon.Click += OnIconClick;

            AddView(icon);
        }

        private void InitTextView()
        {
            var chipTextView = new TextView(Context);

            var chipTextParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            if (_hasIcon || _closable || _selectable)
            {
                chipTextParams.AddRule(Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr1 ? LayoutRules.EndOf : LayoutRules.RightOf, ImageId);
                chipTextParams.AddRule(LayoutRules.CenterVertical);
            }
            else
            {
                chipTextParams.AddRule(LayoutRules.CenterVertical);
            }

            int startMargin = _hasIcon
                ? (int)Resources.GetDimension(Resource.Dimension.chip_icon_horizontal_margin)
                : (int)Resources.GetDimension(Resource.Dimension.chip_horizontal_padding);
            int endMargin = _closable || _selectable ? 0 : (int)Resources.GetDimension(Resource.Dimension.chip_horizontal_padding);
            chipTextParams.SetMargins(startMargin, 0, endMargin, 0);

            chipTextView.LayoutParameters = chipTextParams;
            chipTextView.SetTextColor(new Color(_clicked? _selectedTextColor : _textColor));
            chipTextView.Text = _chipText;
            chipTextView.Id = TextId;

            AddView(chipTextView);
        }

        private void InitBackgroundColor()
        {
            var bgDrawable = new PaintDrawable(new Color(_clicked? _selectedBackgroundColor : _backgroundColor));
            bgDrawable.SetCornerRadius(Resources.GetDimension(Resource.Dimension.chip_height) / 2);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
            {
                Background = bgDrawable;
            }
            else
            {
                SetBackgroundDrawable(bgDrawable);
            }
        }

        private void InitTypedArray(IAttributeSet attrs)
        {
            var ta = Context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.Chip, 0, 0);

            _chipText = ta.GetString(Resource.Styleable.Chip_mcv_chipText);
            _hasIcon = ta.GetBoolean(Resource.Styleable.Chip_mcv_hasIcon, false);
            _chipIcon = ta.GetDrawable(Resource.Styleable.Chip_mcv_chipIcon);
            _closable = ta.GetBoolean(Resource.Styleable.Chip_mcv_closable, false);
            _selectable = ta.GetBoolean(Resource.Styleable.Chip_mcv_selectable, false);
            _backgroundColor = ta.GetColor(Resource.Styleable.Chip_mcv_backgroundColor, ContextCompat.GetColor(Context, Resource.Color.colorChipBackground));
            _selectedBackgroundColor = ta.GetColor(Resource.Styleable.Chip_mcv_selectedBackgroundColor, ContextCompat.GetColor(Context, Resource.Color.colorChipBackgroundClicked));
            _textColor = ta.GetColor(Resource.Styleable.Chip_mcv_textColor, ContextCompat.GetColor(Context, Resource.Color.colorChipText));
            _selectedTextColor = ta.GetColor(Resource.Styleable.Chip_mcv_selectedTextColor, ContextCompat.GetColor(Context, Resource.Color.colorChipTextClicked));
            _closeColor = ta.GetColor(Resource.Styleable.Chip_mcv_closeColor, ContextCompat.GetColor(Context, Resource.Color.colorChipCloseInactive));
            _selectedCloseColor = ta.GetColor(Resource.Styleable.Chip_mcv_selectedCloseColor, ContextCompat.GetColor(Context, Resource.Color.colorChipCloseClicked));

            ta.Recycle();
        }

        #endregion

        protected virtual void OnCloseClick(object sender)
        {
            Close?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnSelectClick(object sender, bool selected)
        {
            Select?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnIconClick(object sender, EventArgs args)
        {
            IconClick?.Invoke(sender, args);
        }
    }
}
