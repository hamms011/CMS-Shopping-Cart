/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

    var roxyFileman = '/fileman/index.html?integration=ckeditor';
    config.filebrowserBrowseUrl = roxyFileman;
    config.filebrowserImageBrowseUrl = roxyFileman + '&type=image';
    config.removeDialogTabs = 'link:upload;image:upload';


    config.extraAllowedContent = '*(*);*{*}';

    $.each(CKEDITOR.dtd.$removeEmpty, function (i, value) {
        CKEDITOR.dtd.$removeEmpty[i] = false;
    });
    config.contentsCss = ['/FrontAssets/css/bootstrap.min.css', '/FrontAssets/css/magnific-popup.css', '/FrontAssets/css/font-icons.css', '/FrontAssets/css/sliders.css', '/FrontAssets/css/style.css', '/FrontAssets/css/animate.min.css'];

    CKEDITOR.scriptLoader.load(['/FrontAssets/js/jquery.min.js', '/FrontAssets/js/bootstrap.min.js', '/FrontAssets/js/plugins.js','/FrontAssets/js/scripts.js']);
};

