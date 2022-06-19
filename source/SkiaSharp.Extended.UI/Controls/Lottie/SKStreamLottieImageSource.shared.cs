﻿namespace SkiaSharp.Extended.UI.Controls;

public class SKStreamLottieImageSource : SKLottieImageSource
{
	public Func<CancellationToken, Task<Stream?>>? Stream { get; set; }

	public override bool IsEmpty => Stream is null;

	internal override async Task<Skottie.Animation?> LoadAnimationAsync(CancellationToken cancellationToken = default)
	{
		if (IsEmpty || Stream is null)
			return null;

		using var stream = await Stream.Invoke(cancellationToken);

		if (!Skottie.Animation.TryCreate(stream, out var animation))
			throw new ArgumentException($"Unable to load Lottie animation.");

		return animation;
	}
}
