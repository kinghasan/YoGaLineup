# Aya Game Tools -- Aya Plugins —— Persistent

# 数据持久化模块

## MultiSave
多存档解决方案，仅建议中大型单机游戏使用，存档数据量较小或者单一存档的情况下建议直接使用 SaveData 接口。
多存档依赖 SaveData 的 module 功能实现，如果启用了 MultiSave，则需要避免直接调用 SaveData 带有 module 参数的接口避免产生数据写入到错误的存档中。
多存档模式下，会额外存出一份存档列表文件，存档列表本身的操作是同步的，但具体存档内容的操作可以是异步的。

## 多文件存储
SaveData 会以 Module 参数作为文件名来将存档数据分为多个文件进行存储，在使用 MultiSave 时，依旧支持将单个存档的不同功能模块分文件存储。
在 MultiSave 中，单文件模式直接以存档 ID 作为 SaveData 的 Module,而多文件模式则使用 ID_模块名 的方式作为 SaveData 的 Module。
此时多个以 `ID_` 开头的不同后缀的文件，共同组成了同一份存档。做存档清理和转移操作时需要注意。

## SaveData
在使用之前需要指定存储类型，默认存储类型为 PlayerPrefs，默认不开启加密。调试模式使用文本方式存储和非加密模式，可以预览存档文件内容。
### PlayerPrefs
基于 Aya.Data.Json 实现对象序列化，基于 Unity 自带的 PlayerPrefs 存储接口进行存储，不支所有持异步处理接口，会自动调用同步实现。
### Json
基于 Aya.Data.Json 定制 Json 库实现序列化，使用纯文本方式存储。
### Serialization
基于 .Net BinaryFormatter 实现二进制序列化，不支持序列化的对象类型会使用 Json 序列化，使用二进制文件方式存储。
### Xml
基于 .Net XmlDocument 实现序列化，不支持序列化的对象类型会使用 Json 序列化，使用纯文本方式存储。

## API
### Init
可以选择两种方式初始化存储模块，第一种方式只指定实现方式，不开启加密。
```csharp
SaveData.Type = SaveType.Json;
ISaveData save = SaveData.CreateSaveDataInstance(SaveType.Json, true);
```
### Get / Set
* Get / Set Value : 用于存取值类型，包括 int / float / double / string 等
* Get / Set Object : 用于存取对象类型，一般为 class

### Load / Save
Load 可以预先加载存档文件，使 Get 接口调用时获得更快的响应，即使没有调用过 Load 接口也可以直接使用 Get。
建议大型存档文件在数据被使用前预先加载，并在必要时统一存储。
同步存取存档，PlayerPrefs 不支持 Load 接口，直接使用 Get 获取数据。

### Load Async / Save Async
异步方式存取存档文件，PlayerPrefs 不支持所有异步接口。

### Module
* 所有API均可选 module 模块参数，但是否使用模块参数需要在项目最初就做出决定。
* 如果使用模块，则存档文件会按模块名存储为多个文件，如果不使用，则会按照项目名称默认存储为单个文件。

## Unity Data Type
存档功能支持一部分 Unity 数据类型，甚至提供了 Texture2D 对象的序列化存储支持，用于保存游戏进度截图等功能需求，但需要注意这些类型的存取操作无法在子线程中进行。

## SaveValue
* sInt
* sFloat
* sBool
* sString

## SaveObject
* sOject

## SaveCollection
* sList
* sDictionary
 