# Release SDK 总览
## 功能
SDK 中间件包含 AD(广告)、IAP(应用内支付)、Analysis(统计分析)、SDK(渠道SDK) 的原生实现隔离。
通过 SDKManager 来调用具体平台对应功能模块的原生实现，游戏逻辑中不关心平台特性。
## Andorid
将 SDK 中间件 SDKManagerImpl.aar 复制到 _Release/Plugins/Android 中。
## iOS
暂未实现
## 使用
在 Unity 项目中，以 XXXManager.Instance.xxx() 的格式调用

# IAPManager
## 启用
* `通过全局宏 IAP 开启内购功能`
## 接入配置
* 配置 _Release/Resources/IAPConfig 中的品列表
## Google Play IAP 的配置
* 配置 _Release/Resources/IAPConfig 中的 Base64EncodedPublicKey，非 AD 类商品需与 Google 后台一致
* 配置 Plugins/Android/res/vaules/google-iap.xml 中的 app_id
### 普通商品(可重复购买)
* 消耗性商品，比如金币 钻石
* ID 需要与 开发者后台 一致
### 一次性购买商品
* 功能性商品，比如去广告
* 去广告功能的启用状态存储值为 IAP_RemoveAD，去除广告类型在 ADConfig 中配置
### 恢复购买
* 仅可以恢复 一次性购买 / 订阅 / 未发货的可重复购买商品
## Google Play IAP 测试方法
* 确定应用最终包名,上传带 `com.android.vending.BILLING` 权限的包
* 确定版本号，与本地测试包一致，发布 Alpha 版本
* 添加测试账号到测试人员列表，并用该账号从测试链接登录一次

# SDKManager
## 启用
* `通过全局宏 SDK 开启SDK功能`
## 接入配置

# ADManager
## 启用
* 无需配置，由应用内逻辑控制
## 接入配置 ADConfig
* 配置 ADConfig 中的广告渠道参数
* 配置 IAP Remove AD 内购去广告功能可以移除的广告类型
* 内购去广告功能的启用状态存储值为 IAP_RemoveAD，不可修改

# AnalysisManager
## 启用

## 接入配置