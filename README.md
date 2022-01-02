[TOC]
# UnityFrameTools
总结遇到的Unity框架以及其中一些非常好用的组件。根据我的目前情况进行了一定的取舍，把其中模块化的功能进行提取。
## 一、关键模块
### 1、单例系统
普通单例 以及Mono单例。
直接将SingleTon文件夹复制到新项目，使用时只需要继承泛型单例即可。大多数模块都需要单例模式，
### 2、资源管理工具
统一的方式加载资源，以及切换不同加载方式。
Res文件夹复制到新项目即可。
### 3、Mono工具
Tools下Mono文件夹。下面的模块默认都已经导入单例模块、资源管理器和Mono管理器。
### 4、事件系统
一个分发消息、响应消息的系统。
#### 消息系统一【简易版】
简易版更轻量级
##### 使用
注册响应事件
```
EventCenter.GetInstance().AddEventListener<KeyCode>(EventCfg.PRESS_A_KEY, PressAKey);
```
触发事件
```
if (Input.GetKeyDown(key))
EventCenter.GetInstance().EventTrigger<KeyCode>(EventCfg.PRESS_A_KEY, key);

```

#### 消息系统二【复杂版】
复杂版使用和调试更方便，功能更加强大

[说明文档](https://github.com/xyzsangxuan/UnityFrameTools/blob/main/Assets/CommonTools/EasyMessage/README.md)



音乐模块需要用到资源加载模块和Mono类模块，没有必要为了降低耦合拆分成独立的。找到Res文件夹、Mono文件夹和Music文件夹导入新项目即可。使用方法看代码。
PS.有些静态路径自己手动切换为项目具体路径。

### 5、存档管理工具
进行本地存档和读档
Tools下Save文件夹

[说明文档](https://github.com/xyzsangxuan/UnityFrameTools/blob/main/Assets/CommonTools/Save/README.md)
### 6、场景管理工具
便于管理场景，进行场景切换和加载

Tools/Scenes文件夹，使用看代码
### 7、UI模块
使用更加合理的方式管理的UI的加载、关闭以及层级等

将Tools/UI文件夹复制到新项目，使用时，将UI继承自UIBase，使用UIMgr进行相关操作。
### 8、实机Debug工具
#### 使用
直接将Tools文件夹下的DebugConsole文件夹和SingleTon文件夹复制到新项目中即可使用
```
调用下面方法即可使用
DebugConsole.StartDebugConsole(false);

```
## 二、主要模块

### 1、表格配置工具
方便策划进行数值相关的配置。
#### 表格工具一【二进制版】
##### 如何使用
* 1: 把Asset/Tools/Config文件夹和singleTon文件夹复制到新项目中。
* 2: 把/Config文件夹复制到根目录下。（这个文件夹存放csv表格文件。也可以直接在项目根目录下新建一个Config文件夹，或者修改表格载入路径）

|ID|名字|描述|时间线
|-|-|-|-|
|id|name|desc|scriptType|
|1|火球|一个火球|SkillFireBallLogic|

第一行是备注

第二行是变量名称

第三行开始是具体数据

* 3: 新建对应实体
```
//技能表
public class SkillDatabase : TableDatabase
{
    //ID
    public string name;
    public string desc;
    public string scriptType;
    public string timeLine;
    //数值统一配置
    public float damage;
    public float castRange;
    public float preTime;
    public float CD;
    public float damageRange;
    public float duringTime;
    public float cost;
    
}

public class SkillTable : ConfigTable<SkillDatabase,SkillTable>
{
    
    
}
```
* 4：使用
点击菜单栏Custom/ConfigToResources
之后会在Resources/Config文件夹下生成一系列二进制文件
使用的时候按照下面代码获取对应行数的数据
```
var tableData = SkillTable.GetInstance()[skillId];
```
PS.在csv表格打开时，运行game会报错，这是因为在编辑器模式下我们直接读取的表格，并不是直接读取生成的二进制文件文件。
####  表格工具二【Scriptable版】

[参考文档](https://github.com/xyzsangxuan/UnityFrameTools/blob/main/Assets/CommonTools/ReadExcelForUnity/README.md)
### 2、缓存池工具
当需要频繁生成大量的物体譬如子弹时进行内存和CPU的资源节省

Tools下Pool文件夹，使用方式看代码


### 3、FSM状态机工具
Tools/FSM文件夹下
### 4、对话系统
Tools/DialogSystem文件夹
### 5、音乐音效工具
播放背景音乐和播放音效
### 6、虚拟摇杆工具
#### JoystickPack
动态位置可隐藏摇杆

固定位置摇杆

浮动摇杆

可切换模式摇杆（三种之间切换）
### 7、编辑器工具
场景快速切换，作弊工具，地图编辑器等。
### 8、本地化工具
方便在不同地区进行本地化处理
### 9、输入系统
优化当前输入方式、以及根据不同输入方式进行切换的系统。
Unity新的输入系统已经完善，这部分考虑切换为Unity自带的输入系统。移动端和PC端按照需求设计输入系统

### 10、其他常用工具
Tools/Utils&Externs文件夹下。
* 随机数工具
* 时间线工具
* 定时器
* UI相关
* 扩展函数，获取子物体、删除子物体、按钮动效等
* Utils常用功能，包括获取距离，获取纯字符等
...
### 服务器客户端管理工具
用于管理资源载入、资源热更新、脚本热更新、用户数据流等
### 交互管理模块
使用统一的方式管理场景内物体的交互
### 数据库解决方案
### 寻路工具
### 大数工具
### 地图编辑器
### 静态变量管理工具


## 三、需要常备的插件工具
### 1、DoTween
更方便的处理UI动画和一些简单的动画。

### 2、odin
极其强大的编辑器工具

接下来的工作：

从RPG框架中把技能模块抽离出来；

把服务器和客户端模块抽离出来

完整版请看我参考的框架：

https://github.com/xyzsangxuan/RPGFramework

https://github.com/xyzsangxuan/UnityFramework

https://github.com/brkdyh/CommonFramework