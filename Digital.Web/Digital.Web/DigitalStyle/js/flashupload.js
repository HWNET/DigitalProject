//使用方法：
//1.页脚引入：
//<script src="<%=Url.StaticFile("/Content/Scripts/flashupload.js") %>" type="text/javascript"></script>
//2.页面插入元素：
//<div id="uploader"></div>
//3.上传JS代码事例：
/*
    $('#uploader').FileUpload({
        'subfolder': 'unit', 
        'callback' : function(msg) {
            //msg.url为图片绝对地址
        }
    });

    //需要定制化请使用下面的：
    $('#uploader').FileUpload({
        'subfolder': 'unit', 
        'thumbs': 's,40,30,Cut|m,500,340,Cut',
        'width': 66,
        'height': 22,
        'buttonImage': staticFileRoot + '/Common/Uploadify/uploadbutton.png',
        'method': 'GET',
        'queueID': 'fileQueue',
        'fileSizeLimit': '5MB',
        'fileTypeDesc': "jpg/png/gif Files",
        'fileTypeExts': '*.gif; *.jpg; *.png',
        'auto': true,
        'multi': true,
        'onUploadStart' : function(file) {
            alert('Starting to upload ' + file.name);
        },
        'onSelect' : function(file) {
            alert('The file ' + file.name + ' was added to the queue.');
        },
        'callback' : function(msg) {
            alert("原始图片：" + msg.url + "\r\n"
                + "m缩略图：" + getthumpath(msg.url, "m") + "\r\n"
                + "s缩略图：" + getthumpath(msg.url, "s"));
        }
    });
*/

if ("undefined" != typeof staticFileRoot) {
    document.write('<script type="text/javascript" src="' + staticFileRoot + '/DigitalStyle/js/plupload/js/plupload.full.min.js"></script>');
    document.write('<script type="text/javascript" src="' + staticFileRoot + '/DigitalStyle/js/plupload/js/i18n/zh_CN.js"></script>');
    try {



        $.fn.FileUpload = function (param) {

            var uploader = new plupload.Uploader({
                runtimes: 'html5,flash,silverlight,html4',
                browse_button: 'pickfiles', // you can pass in id...
                container: document.getElementById('container'), // ... or DOM Element itself
                url: staticFileRoot + "/FileUpload.ashx?subfolder=" + param["subfolder"] + "&" + "thumbwidth=" + param["thumbwidth"] + "&" + "thumbheight=" + param["thumbheight"] + "&" + "mode=" + param["mode"] + "&" + "ImageId=" + param["ImageId"] + "&" + "subSubFolderCode=" + param["subSubFolderCode"],
                flash_swf_url: staticFileRoot + '/DigitalStyle/js/plupload/js/Moxie.swf',
                silverlight_xap_url: staticFileRoot + '/DigitalStyle/js/plupload/js/Moxie.xap',

                filters: {
                    max_file_size: '10mb',
                    mime_types: [
                        { title: "Image files", extensions: "jpg,gif,png" },
                        { title: "Zip files", extensions: "zip" }
                    ]
                },
               
                init: {
                    PostInit: function () {
                        document.getElementById('filelist').innerHTML = '';

                        document.getElementById('uploadfiles').onclick = function () {
                            uploader.start();
                            return false;
                        };
                    },

                    FilesAdded: function (up, files) {
                        plupload.each(files, function (file) {
                            document.getElementById('filelist').innerHTML = '<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></div>';
                        });
                    },

                    UploadProgress: function (up, file) {

                        document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
                    },
                    FileUploaded: function (uploader, file, rep) {
                        var msg = JSON.parse(rep.response);
                        callback(msg);
                       // eval(param["callback"] + "(" + msg + ")");
                       
                    },
                    Error: function (up, err) {
                        alert("\nError #" + err.code + ": " + err.message);
                        //document.getElementById('console').innerHTML += "\nError #" + err.code + ": " + err.message;
                    }

                }
            });
            uploader.init();

        }
    }
    catch (e) {

    }

} else {
    alert("请在网站母板页配置全局的'staticFileRoot'");
}

function getThumbAbsoluteUrl(url, suffix) {
    suffix = suffix || "s";
    var ext = url.substring(url.lastIndexOf('.'));
    var head = url.substring(0, url.lastIndexOf('.'));
    url = head + "_" + suffix + ext;
    if (url.indexOf("http") != 0)
        url = staticFileRoot + url;

    return url;
}