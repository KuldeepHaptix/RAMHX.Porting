CKEDITOR.config.inlinesave = {
    postUrl: '/admin/HtmlModules/SaveHtmlContent',
    postData: { htmlmoduleid: '1', htmldata: 's' },
    onSave: function (editor) {
        //console.log('clicked save', editor, editor.getData(), editor.config.inlinesave);
        editor.config.inlinesave.postData = { 'htmlmoduleid': editor.name.split('_')[1], 'htmldata': editor.getData() };
        return true;
    },
    onSuccess: function (editor, data) {
        console.log('save successful', editor, data);
    },
    onFailure: function (editor, status, request) {
        console.log('save failed', editor, status, request);
    },
    useJSON: true,
    useColorIcon: false
};

CKEDITOR.config.extraPlugins = 'inlinesave';//savebtn is the plugin's name


