# Limlzy.VerificationCode
一款C#的验证码生成组件，支持使用中文，英文，数字生成验证码并且支持混淆设置。

#### 怎么使用？
```
//最简单的：
var creator = new Limlzy.VerificationCode.VerificationCodeCreator();
creator.CreateImage();
creator.Image.Save("code.png"); 
//creator.Image属性就是生成的图片对象，你可以做任何可以对Image对象所做的事。
//creator.CodeString 就是随机生成的二维码字符串
//生成英文和数字的：
creator.Charset = VerificationCode.CharSet.English | VerificationCode.CharSet.Number;
creator.CreateImage();
//生成指定长度的验证码：
creator.CreateImage(20);
//以上生成的都是大小自动并且随机的验证码
//当然使用自定义字符串时len参数无效，嗯虽然不好看 懒得改了。。 
creator.CreateImage(10, "jadwxalkjx");
```
祝你使用愉快，愉快的改代码。。。

项目具体介绍：

http://www.coket.me/2017/03/31/打造一款轻量级的c验证码生成器


博客：

http://www.coket.me/   
