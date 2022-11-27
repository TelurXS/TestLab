
const image_preview = $("#image-preview");
const image_input = $("#image-input");

const UpdateImage = (e) =>
{
    var value = e.target.value;
    console.log(value);
    console.log(image_preview);
    image_preview.attr("src", value);
}

image_input.change(UpdateImage);