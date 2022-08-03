# UTween - Structure

## UTween Animation
可用于直接在编辑器中配置多时间轴动画效果的组件，包含一个 TweenData 和若干个 Tweener，在运行时执行时，TweenData 会被传递给 UTween Mananger 执行，而在编辑器预览时，由 UTween Animation 自身执行。

## UTween Manager
所有动画效果的实际执行类，用于生成、回收、播放所有的运行时动画效果。

## Tween Data
包含一个 Tweener 列表，和公用的动画基础参数。每个 Tweener 至少对应一个 TweenData，是动画的最小执行单元，UTweenAnimation 和 UTweenAnimationAsset 等组件都会包含一个 TweenData 用于在不同的使用场景下实现动画效果，同一个 TweenData 可以通过代码传递到不同的地方被多次使用。

## Tweener
单一动画效果实现的最小单元，与所属的 TweenData 一起构成一个完整的动画。

## UTween Animation Asset
可重复使用的动画配置资源文件，包含一个 TweenData 和若干个 Tweener，可从 UTween Animation 导出。

## UTween
大部分动画都同时拥有组件和代码两种使用方式的实现，支持代码调用的基础动画和官方扩展动画，都会提供包含在 UTween 类中的统一风格的静态调用接口。