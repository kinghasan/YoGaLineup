# Level 关卡结构

## Level Manager
1.关卡管理器，当 `Test Level Index` 大于 0 时，则从指定的关卡开始运行，用于测试，否则读取存档中的关卡号继续游戏。
2.当 `Start Rand Index` 大于 0 时，游戏进行到该参数指定的关卡后，后续关卡会从 `Rand List` 中的关卡随机获取，用来实现无尽关卡。

## Level
每个关卡预制的根节点需要挂在此脚本，用来管理当前关卡的所有路段(`Level Block`)、道具(`Item Base`)以及一些关卡相关参数。
`Test Create` 按钮可以生成预览当前关卡的布局，但需要在保存预制之前使用 `Destroy` 按钮进行清除。

## Level Block
构成关卡的路段，每个路段至少需要包含一个路径(`Level Path`)，用来指定关卡的拼接起点和终点。

## Level Path
### Level Path Line
直线路径，需要指定一个起点和终点。
### Level Path Spline
自定义路径，依赖 Spline 插件实现，可以是直线或者曲线，需要注意的是，为了保持拼接方向正确，曲线结束端最后的两个点用来确定拼接方向。

## Path Follower
每个玩家的 `Player Move` 会包含一个对应的路径跟随器组件，用来计算和执行沿着当前关卡的多个路段所构成的路径移动。