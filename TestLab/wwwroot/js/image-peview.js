
const image_preview = $("#image-preview");
const image_input = $("#image-input");

const UpdateImage = (e) =>
{
    var value = URL.createObjectURL(e.target.files[0]);
    image_preview.attr("src", value);
}

image_input.change(UpdateImage);