var gulp = require('gulp');
var sass = require('gulp-sass');
var cleanCSS = require('gulp-clean-css');

gulp.task('sass', function () {
    return gulp.src('./style.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(cleanCSS({level: 2}))
        .pipe(gulp.dest('./wwwroot'));
});

gulp.task('sass:watch', function () {
    gulp.watch('./style.scss', ['sass']);
});