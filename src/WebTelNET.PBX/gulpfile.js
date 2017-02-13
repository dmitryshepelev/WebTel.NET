/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var gulpFileCopy = require('gulp-file-copy');
var gulpScss = require('gulp-scss');
var gulpCssmin = require('gulp-cssmin');
var gulpRename = require('gulp-rename');


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

var root = 'wwwroot/styles/';

gulp.task('compile-scss', function() {
    gulp.src([root + 'styles.scss', root + 'sidepanel.scss'])
        .pipe(gulpScss())
        .pipe(gulp.dest(root));
});

gulp.task('min-css', function() {
    gulp.src([root + 'styles.css', root + 'sidepanel.css'])
        .pipe(gulpCssmin())
        .pipe(gulpRename({suffix: '.min'}))
        .pipe(gulp.dest(root));
});

gulp.task('compile-and-min-scss', ['compile-scss', 'min-css']);
