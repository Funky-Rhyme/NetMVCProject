(function () {
  $('body').on('change', '#Directions', function () {
    $('#Directions').attr('disabled', 'disable');
    $('.cources-container').html('<div class="alert alert-light" role="alert">' +
      'Загрузка...' +
      '</div >')
    Api.get('/Home/getCoursesData', {
      directionName: $('#Directions option:selected').text()
    }).done(function (data) {
      if (data) {
        $('.cources-container').empty();
        $('.cources-container').append(data);
        $('#Directions').removeAttr('disabled');
      }
    });
  });

  $('body').on('click', '.addSection', function () {
    let section = $('.section-template .section').clone();
    section.find('label').eq(0).text('Раздел ' + ($(this).parent().find('.section').length + 1) + '.');
    section.find('input').eq(0).text('');
    $(this).parent('.course-container').find('.del-section').hide();
    section.insertBefore($(this));
  });

  $('body').on('click', '.del-section', function () {
    let delButtons = $(this).parent().parent().find('.del-section');
    delButtons.eq(delButtons.length - 2).show();
    $(this).parent().remove();
  });

  $('body').on('click', '.add-topic', function () {
    let section = $('.topic-template .topic').clone();
    section.find('label').eq(0).text('Тема ' + ($(this).parent().find('.topic').length + 1) + '.');
    section.find('input').eq(0).text('');
    section.find('textarea').eq(0).val('');
    $(this).parents('.section').find('.del-topic').hide();
    section.insertBefore($(this));
  });

  $('body').on('click', '.del-topic', function () {
    let delButtons = $(this).parents('.section').find('.del-topic');
    delButtons.eq(delButtons.length - 2).show();
    $(this).parents('.topic').eq(0).remove();
  });

  $('body').on('click', '.generate', function () {
    let coursesData = [];
    $('.cources-container .course-container').each(function () {
        let sections = [];
      coursesData.push(sections);
      $(this).find('.section').each(function (s) {
        let topics = [];
        sections.push({
          Name: $(this).find('input').eq(0).val(),
          Topics: topics
        })
        $(this).find('.topic').each(function (t) {
          topics.push({
            Name: $(this).find('input').val(),
            Description: $(this).find('textarea').val()
          });
        });
      });
    });

    let data = {
      DisciplineName: $('#Discipline').val(),
      DirectionName: $('#Directions option:selected').text(),
      DirectionCode: $('#Directions').val(),
      Courses: coursesData
    };

    console.log(data);

    Api.post('/Home/GenerateDocument', data).done(function (fileName) {
      window.location.href = "/Home/Download?fileName=" + fileName;
    });
  });
})();