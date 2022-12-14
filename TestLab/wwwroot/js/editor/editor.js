
const saveButton = $("#save");
const idInput = $("[name=id]");
const titleInput = $("[name=title]");
const descriptionInput = $("[name=description]");
const imageInput = $("[name=image]");


const AddMessage = (message, type) =>
{
    var holder = $("#messageHolder");
    holder.empty();

    var element = $(
        `
            <div class="alert alert-${type} alert-dismissible fade show">
                ${message}
                <button type="button" class="close small" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        `);

    holder.append(element);
}

ClassicEditor
    .create(document.getElementById("editor"), {
        toolbar: {
            items: [
                'heading',
                'fontFamily',
                'fontSize',
                'fontBackgroundColor',
                'fontColor',
                '|',
                'bold',
                'italic',
                'underline',
                'strikethrough',
                'bulletedList',
                'numberedList',
                '|',
                'outdent',
                'indent',
                'alignment',
                '|',
                'link',
                'imageInsert',
                'imageUpload',
                'mediaEmbed',
                'blockQuote',
                'insertTable',
                'codeBlock',
                'htmlEmbed',
                'undo',
                'redo'
            ]
        },
        language: 'en',
        image: {
            toolbar: [
                'imageTextAlternative',
                'toggleImageCaption',
                'imageStyle:inline',
                'imageStyle:block',
                'imageStyle:side'
            ]
        },
        table: {
            contentToolbar: [
                'tableColumn',
                'tableRow',
                'mergeTableCells'
            ]
        },
        list: {
            properties: {
                styles: true,
                startIndex: true,
                reversed: true
            }
        },
        heading: {
            options: [
                { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' },
                { model: 'heading3', view: 'h3', title: 'Heading 3', class: 'ck-heading_heading3' },
                { model: 'heading4', view: 'h4', title: 'Heading 4', class: 'ck-heading_heading4' },
                { model: 'heading5', view: 'h5', title: 'Heading 5', class: 'ck-heading_heading5' },
                { model: 'heading6', view: 'h6', title: 'Heading 6', class: 'ck-heading_heading6' }
            ]
        },
        placeholder: 'Content',
        fontFamily: {
            options: [
                'default',
                'Arial, Helvetica, sans-serif',
                'Courier New, Courier, monospace',
                'Georgia, serif',
                'Lucida Sans Unicode, Lucida Grande, sans-serif',
                'Tahoma, Geneva, sans-serif',
                'Times New Roman, Times, serif',
                'Trebuchet MS, Helvetica, sans-serif',
                'Verdana, Geneva, sans-serif'
            ],
            supportAllValues: true
        },
        fontSize: {
            options: [10, 12, 14, 'default', 18, 20, 22],
            supportAllValues: true
        },
        htmlSupport: {
            allow: [
                {
                    name: /.*/,
                    attributes: true,
                    classes: true,
                    styles: true
                }
            ]
        },
        htmlEmbed: {
            showPreviews: true
        },
        link: {
            decorators: {
                addTargetToExternalLinks: true,
                defaultProtocol: 'https://',
                toggleDownloadable: {
                    mode: 'manual',
                    label: 'Downloadable',
                    attributes: {
                        download: 'file'
                    }
                }
            }
        },
    })
    .then(editor =>
    {
        const Save = (e) => {

            var formData = new FormData();
            formData.append("id", idInput.val());
            formData.append("image", imageInput.prop("files")[0]);
            formData.append("title", titleInput.val());
            formData.append("description", descriptionInput.val());
            formData.append("content", editor.getData());

            $.ajax({
                type: 'POST',
                url: "/editor/Save",
                data: formData,

                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                success: function (responce) {
                    AddMessage(`Succsessfully saved <a href='?id=${idInput.val()}'>(Edit)</a>`, "success");
                },
                error: function () {
                    AddMessage("Something wrong", "danger");
                }
            });
        }

        saveButton.click(Save);
    });