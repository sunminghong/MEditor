#软件名称：MDEditor  V0.9.1.0#
##软件用途：markdown格式文档编辑和预览。##
>MEditor is text editor with multiple windows. You can use it as markdown editor and preview. 

- 可应用于写博客，可以写好后将html剪切发布即可；
- 团队协作之间的文档编辑与阅读，纯文本格式，便于版本比较；html预览，便于阅读

##发起人：一挥，[新浪微博](http://weibo.com/5d13 "一挥间的新浪微博"),[博客](http://cnblogs.com/yihuiso "一挥间的博客")
##许可协议：full open source,随便修改##
如果您愿意将您的修改提交，方便所有需要的人，请注意：
-修改lastver.txt文件，请只改版本号；
-请修改一下本文档about.md
-请修改一下README
-如果有语法扩充请修改[语法文档](https://github.com/5d13cn/MEditor/raw/master/resoucesdocs/syntax.md)
##下载地址: [执行档](https://github.com/5d13cn/MEditor/raw/master/exeoutput/MDEditor.exe "码德编辑器下载")

##v0.9.1.0版本的新增修改功能列表
*	\*增加文件内容在外部发生改变时自动提醒。
*	+修改在html预览里的点击链接的处理规则：
	+	本地的.md,.markdown 文件的链接则自动打开此文档并转换成html预览
	+	通过在通用选项里设置文本文档文件扩展名，可以自动打开此文档并预览



##v0.9.0.0版本的新增修改功能列表
*	\*在html预览里的点击本地的.md 文件的链接则自动打开此.md文档；如果是外网的则转换成html格式预览


##V0.8.0.0版本的新增修改功能列表
*	+正式修改软件中文名称为：码德编辑器;可执行主文件名改为MDEditor.exe
*	+修改配置所保存方式，现在如果改了样式，就会在同级目录下生成MDEditor.exe.config,只需要保存此文件再可同步你的样式设置了。
*	+将还原经典白底黑字功能集成到 通用选项窗口
*	+将还原经典黑底白字功能集成到 通用选项窗口
*	\*增加tabpage选择顺序记录功能，当关闭当前编辑的文件时会将上一个页激活，提高体验
*	+解决页面切换后的焦点bug。

##V0.6版本的新增修改功能列表
*	+增加了选中多行时按{Tab}键时可以增加缩进
*	+增加了按{Shirft+Tab}键时减少缩进
*	+增加了html预览样式自定义功能
*	+更改html预览快捷键为F5
*	\*将字体、颜色等集成到"通用选项"里
*	\*修改关闭文档及退出文档时点”取消“按钮会直接退出的bug
*	\*修改制表符（跳格键）为"\t",以前为四个空格
*	\*修改不能拖动md文件到编辑框打开的bug

##V0.6待解解决的bug:
5. 关闭一个tab后，建议回到前一个tab，而不是第一个tab。
7.自动读取文件更新
8.获取焦点上有问题

##要增加的功能##
1. tab空格替代可选，tab字符可设置


##V0.5版的功能列表：##

*    文件操作功能
    +    可以同时打开多个标签
    +    保存文件、保存所有、关闭等
    +    支持拖动文档的方式打开文档
    +    支持windows 7 以前的系统下进行.mark文件格式扩展名关联；windows7下的欢迎各位朋友完善了
*    编辑功能
    +    拷贝、剪切、粘贴，支持windows下的通用快捷键
    +    支持回退、重复
    +    可以对自动换行进行切换
    +    实现了查找、替换功能
    +    可以自定义编辑器前景色和背景色
    +    可以方便的在markdown与html间进行切换
*    以html文档预览markdown，两大主要功能之一
*    辅助功能
    +    默认预览器在右边、编辑器在左边，但为方便不同用户的例用习惯，可以在切换工具菜单下进行左右切换。
*    支持的markdown语法
    +    [校准markdown的语法]()
    +    参考[google code wiki的表格语法] [2],实现了***html table tag***

举例：td内容仍可用markdown语法

        || *Year* || *Temperature (low)* || *Temperature (high)* || 
        || 1900 || -10 || 25 || 
        || 1910 || -15 || 30 || 
        || 1920 || -10 || 32 ||
        || 1930 || _N/A_ || _N/A_ || 
        || 1940 || -2 || 40 ||

|| *Year* || *Temperature (low)* || *Temperature (high)* || 
|| 1900 || we || 25 || 
|| 1910 || -15 || 30 || 
|| 1920 || -10 || 32 ||
|| 1930 || _N/A_ || _N/A_ || 
|| 1940 || -2 || 40 ||

##现有不足（欢迎大家贡献）：##
*    没有实现对玩家设置的一些使用习惯进行保存
*    编辑功能很弱，没有列编辑及tab键多行缩进功能

[1]:http://daringfireball.net/projects/markdown/syntax        "markdown syntax"
[2]: http://code.google.com/p/support/wiki/WikiSyntax#Tables    "table syntax"




