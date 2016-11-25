/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var gulpFileCopy = require('gulp-file-copy');


gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('bootstrap-build',
    function() {
        var bootstrapSrc = './node_modules/bootstrap/scss/';
        var bootstrapCustomFilePath = './wwwroot/styles/_custom.scss';

        gulp.src(bootstrapCustomFilePath)
            .pipe(gulp.dest(bootstrapSrc));
    });

gulp.task('framework-services-build', function () {
    var sourcePath = '../WebTelNET.CommonClient/services';


})