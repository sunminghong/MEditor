##软件名称：MEditor##
##软件用途：markdown格式文档编辑和预览。##
>MEditor is text editor with multiple windows. You can use it as markdown editor and preview. 

- 可应用于写博客，可以写好后将html剪切发布即可；
- 团队协作之间的文档编辑与阅读，纯文本格式，便于版本比较；html预览，便于阅读

##许可协议：full open source,随便修改##
如果您愿意将您的修改提交，方便所有需要的人，请注意：
-修改lastver.html文件，请只改版本号；
-请修改一下本文档about.md
-请修改一下README
-如果有语法扩充请修改syntax.md
        
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




