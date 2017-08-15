$(function(){
    for(var mo=0;mo<$('.momo-js').length;mo++){
        console.log(mo);
        $('.momo-js').eq(mo).attr('data-idx',mo)
    }
    $('.momo-js').click(function(){
        $('.momo ul').hide();
        $('.momo-' + $(this).attr('data-idx')).show();
        $('.tabb').hide();
        $('.tabb').eq($(this).attr('data-idx')).show();
    });
    //刷新页面默认显示ss1
    $(document).ready(function(){
        $('.ss1').show();
    });



});

