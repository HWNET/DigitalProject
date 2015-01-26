/**//**  
* @author ming  
*/  
$(document).ready(function(){       
    /**//* 设置默认属性 */

    //$.validator.setDefaults({       
        //submitHandler: function(form) {    
        //    form.submit();    
        //}
        //highlight: function (element, errorClass, validClass) {
        //    if (element.type === 'radio') {
        //        this.findByName(element.name).addClass(errorClass).removeClass(validClass);
        //    } else {
        //        $(element).addClass(errorClass).removeClass(validClass);
        //        $(element).closest('.form-group').removeClass('success').addClass('error');
        //    }
        //},
        //unhighlight: function (element, errorClass, validClass) {
        //    if (element.type === 'radio') {
        //        this.findByName(element.name).removeClass(errorClass).addClass(validClass);
        //    } else {
        //        $(element).removeClass(errorClass).addClass(validClass);
        //        $(element).closest('.form-group').removeClass('error').addClass('success');
        //    }
        //},
    //});

    //字符验证 input file 文件路径合法
    jQuery.validator.addMethod("validPathCheck", function (value, element) {
        var validPath = "^(([a-zA-Z]:|\\\\)\\\\)?(((\\|<>\\.   ])|([^\\\\/:\\*\\?\"\\|<>\\.   ](([^\\\\/:\\*\\?\"\\|<>\\.   ])|([^\\\\/:\\*\\?\"\\|<>]*[^\\\\/:\\*\\?\"\\|<>\\.   ]))?$";
        var regPath = new RegExp(validPath);
        return this.optional(element) || regPath.test(value);
    }, "只能包括有效文件路径");

    //字符验证 非法字符
    jQuery.validator.addMethod("validCheck", function (value, element) {
        var invalidString =/[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im;
        return this.optional(element) || invalidString.test(value);
    }, "只能包括有效字符");

    // 判断是否包含中英文特殊字符，除英文"-_"字符外
    jQuery.validator.addMethod("isContainsSpecialChar", function (value, element) {
        var reg = RegExp(/[(\ )(\`)(\~)(\!)(\@)(\#)(\$)(\%)(\^)(\&)(\*)(\()(\))(\+)(\=)(\|)(\{)(\})(\')(\:)(\;)(\')(',)(\[)(\])(\.)(\<)(\>)(\/)(\?)(\~)(\！)(\@)(\#)(\￥)(\%)(\…)(\&)(\*)(\（)(\）)(\—)(\+)(\|)(\{)(\})(\【)(\】)(\‘)(\；)(\：)(\”)(\“)(\’)(\。)(\，)(\、)(\？)]+/);
        return this.optional(element) || !reg.test(value);
    }, "含有中英文特殊字符");

// 数字验证 正浮点数
jQuery.validator.addMethod("floatCheck", function (value, element) {
        var floatNumber = /^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$/
        return this.optional(element) || floatNumber.test(value);
}, "只能包括数字或者小数位数字");

// 数字验证 数字 银行账号
jQuery.validator.addMethod("bankCheck", function (value, element) {
    return this.optional(element) || /^\d{19}$/g.test(value);
}, "只能包括19位数字");

// 字符验证 企业网址
jQuery.validator.addMethod("siteCheck", function (value, element) {
    var siteUrl = "^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$";
    var regUrl = new RegExp(siteUrl);
    return this.optional(element) || regUrl.test(value);
}, "只能是一个合法有效的网址");

// 字符验证       
jQuery.validator.addMethod("stringCheck", function (value, element) {
    return this.optional(element) || /^[\u0391-\uFFE5\w]+$/.test(value);       
}, "只能包括中文字、英文字母、数字和下划线");

// 字符验证 中文字      
jQuery.validator.addMethod("stringCNNCheck", function (value, element) {
    return this.optional(element) || /^[\u4E00-\u9FA5]+$/.test(value);
}, "只能包括中文字");

// 字符验证 中文字 , 限制500字以内      
jQuery.validator.addMethod("stringCNNLimitCheck", function (value, element) {
    return this.optional(element) || /^[\u4E00-\u9FA5]+,{1,500}$/.test(value);
}, "只能包括中文字,请确保输入500字以内");

    // 字符验证 字符串, 限制500字以内 , 汉字双字节     
jQuery.validator.addMethod("stringContentCheck", function (value, element, param) {
    var length = value.gblen();

    return this.optional(element) || (length >= param[0] && length <= param[1]);
}, $.validator.format("请确保内容输入{0}-{1}字以内"));

// 字符验证 英文字母，数字       
jQuery.validator.addMethod("stringNUCheck", function (value, element) {
    return this.optional(element) || /^[A-Za-z0-9]+$/.test(value);
}, "只能包括英文字母和数字");

    // 字符验证 数字       
jQuery.validator.addMethod("numberCheck", function (value, element) {
    return this.optional(element) || /^[0-9]+$/.test(value);
}, "只能包括数字");

// 中文字符验证       
jQuery.validator.addMethod("stringCNCheck", function (value, element) {
    return this.optional(element) || /^[\u4E00-\u9FA5]+|[\u4E00-\u9FA5]+\(?[\u4E00-\u9FA5]+\)?$/.test(value);
}, "只能包括中文字、( 和 )");

// 中文字两个字节  限制500字以内
jQuery.validator.addMethod("byteRangeCNLength", function (value, element, param) {
    var length = value.length;
    for (var i = 0; i < value.length; i++) {
        if (value.charCodeAt(i) > 127) {
            length++;
        }
    }
    return this.optional(element) || (length >= param[0] && length <= param[1]);
}, "请确保输入500字以内(一个中文字算2个字节)");

// 中文字两个字节       
jQuery.validator.addMethod("byteRangeLength", function(value, element, param) {       
    var length = value.length;       
    for(var i = 0; i < value.length; i++){       
        if(value.charCodeAt(i) > 127){       
            length++;       
        }       
    }       
    return this.optional(element) || ( length >= param[0] && length <= param[1] );       
    }, "请确保输入的值在3-15个字节之间(一个中文字算2个字节)");   
 
// 身份证号码验证       
jQuery.validator.addMethod("isIdCardNo", function(value, element) {       
    return this.optional(element) || isIdCardNo(value);       
    }, "请正确输入您的身份证号码");    

    // 匹配qq      
jQuery.validator.addMethod("isQq", function (value, element) {
    return this.optional(element) || /^[1-9]\d{4,12}$/;
}, "匹配QQ");

// 手机号码验证       
jQuery.validator.addMethod("isMobile", function(value, element) {       
    var length = value.length;   
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;   
    return this.optional(element) || (length == 11 && mobile.test(value));       
    }, "请正确填写您的手机号码");       
    
// 电话号码验证       
jQuery.validator.addMethod("isTel", function(value, element) {       
    var tel = /^\d{3,4}-?\d{7,9}$/;    //电话号码格式010-12345678   
    return this.optional(element) || (tel.test(value));       
    }, "请正确填写您的电话号码");   

// 联系电话(手机/电话皆可)验证   
jQuery.validator.addMethod("isPhone", function(value,element) {   
    var length = value.length;   
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;   
    var tel = /^\d{3,4}-?\d{7,9}$/;   
    return this.optional(element) || (tel.test(value) || mobile.test(value));   
    }, "请正确填写您的联系电话");   

// 邮政编码验证       
jQuery.validator.addMethod("isZipCode", function(value, element) {       
    var tel = /^[0-9]{6}$/;       
    return this.optional(element) || (tel.test(value));       
    }, "请正确填写您的邮政编码");    

    //身份证号码的验证规则
function isIdCardNo(num) {
    //if (isNaN(num)) {alert("输入的不是数字！"); return false;} 
    var len = num.length, re;
    if (len == 15)
        re = new RegExp(/^(\d{6})()?(\d{2})(\d{2})(\d{2})(\d{2})(\w)$/);
    else if (len == 18)
        re = new RegExp(/^(\d{6})()?(\d{4})(\d{2})(\d{2})(\d{3})(\w)$/);
    else {
        //alert("输入的数字位数不对。"); 
        return false;
    }
    var a = num.match(re);
    if (a != null) {
        if (len == 15) {
            var D = new Date("19" + a[3] + "/" + a[4] + "/" + a[5]);
            var B = D.getYear() == a[3] && (D.getMonth() + 1) == a[4] && D.getDate() == a[5];
        }
        else {
            var D = new Date(a[3] + "/" + a[4] + "/" + a[5]);
            var B = D.getFullYear() == a[3] && (D.getMonth() + 1) == a[4] && D.getDate() == a[5];
        }
        if (!B) {
            //alert("输入的身份证号 "+ a[0] +" 里出生日期不对。"); 
            return false;
        }
    }
    if (!re.test(num)) {
        //alert("身份证最后一位只能是数字和字母。");
        return false;
    }
    return true;
}


});