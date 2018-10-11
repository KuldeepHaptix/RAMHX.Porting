
function applyCKEditor () 
{
	if (GetQSValue('rh_mode') == 'edit') {
		console.log('page edit mode on - Start')

		//	CKEDITOR.config.extraPlugins = 'inlinesave';
		//CKEDITOR.disableAutoInline = true;

		//CKEDITOR.inline();

		CKEDITOR.disableAutoInline = true;
		$(".editModule").each(function (index) {
			var content_id = $(this).attr('id');
			var tpl = $(this).attr('tpl');

			CKEDITOR.inline(content_id, {
				on: {
					blur: function (event) {
						var data = event.editor.getData();
						console.log('123')
						var request = jQuery.ajax({
							url: "/admin/cms-pages/inline-update",
							type: "POST",
							data: {
								content: data,
								content_id: content_id,
								tpl: tpl
							},
							dataType: "html"
						});
					}
				}
			});
		});
	}
}