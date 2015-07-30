(function () {

    var app = angular.module('photo', []);

    function sortInfoTags() {
        return function (items) {
            items = items === undefined ? [] : items;

            items.sort(function (a, b) {
                return a.Tag.TagType.ID > b.Tag.TagType.ID ? 1 : -1;
            });
            return items;
        };
    }

    function GalleryController($http) {
        var vm = this;
        vm.TagTypes = [];
        vm.Images = [];
        vm.Info = {};

        var screenWidth = screen.width;
        var screenHeight = screen.height;
        var devicePixelRatio = window.devicePixelRatio || 1;
        var thumbnailSize = 128;

        function loadNavbar(data) {
            vm.TagTypes = data.TagTypes;
        }

        function loadGallery(data) {
            vm.Images = data;
        }

        function loadInfo(data) {
            vm.Info = data;
        }

        vm.getThumbnail = function getThumbnail(image) {
            var url = 'image?id=' + image.ID + '&x=' + (thumbnailSize * devicePixelRatio) + '&y=' + (thumbnailSize * devicePixelRatio);
            return url;
        };

        vm.getImage = function getImage(image) {
            $http.get('/photo/info?id=' + image.ID).success(loadInfo);
            var url = 'image?id=' + image.ID + '&x=' + (screenWidth * devicePixelRatio) + '&y=' + (screenHeight * devicePixelRatio);
            $('.lightbox').css('background-image', 'url("' + url + '")');
        };

        vm.formatDate = function formatDate(dateString) {
            var dateTaken = new Date(dateString);
            var year = dateTaken.getFullYear();
            var month = dateTaken.getMonth() + 1;
            var day = dateTaken.getDate();
            return (month < 10 ? '0' + month : month) + '/' + (day < 10 ? '0' + day : day) + '/' + year;
        };

        vm.setFilter = function setFilter(tag) {
            $http.get('/photo/images?id=' + tag).success(loadGallery);
        };

        $http.get('/photo/navbar').success(loadNavbar);
        $http.get('/photo/images').success(loadGallery);
    }

    app.filter('sortInfoTags', sortInfoTags);
    app.controller('gallery', ['$http', GalleryController]);

})();