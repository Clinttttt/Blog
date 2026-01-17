namespace Blog.Client.Helper
{
    public static class TinyMCEConfig
    {
        public static Dictionary<string, object> GetConfig()
        {
            return new Dictionary<string, object>
            {
                { "menubar", false },
                { "branding", false },
                { "resize", false },
                { "promotion", false },
                { "statusbar", false },
                { "highlight_on_focus", false },
                { "placeholder", "Start writing your content here..." },
                
                // PLUGINS - TinyMCE 8.0 compatible (free version only)
                { "plugins", "lists link image code codesample table autoresize visualblocks fullscreen searchreplace wordcount anchor charmap" },
                
                // TOOLBAR - Enhanced with font size, more formatting options
                { "toolbar", "undo redo | styles fontsize | bold italic underline strikethrough | forecolor backcolor | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image table horizontalrule | codesample blockquote | searchreplace charmap anchor | fullscreen | removeformat code" },

                // Prevent toolbar wrapping and use sliding drawer for overflow
                { "toolbar_drawer", "sliding" },
                { "toolbar_mode", "sliding" },

                // FONT SIZE OPTIONS
                { "fontsize_formats", "8pt 10pt 12pt 14pt 16pt 18pt 20pt 24pt 28pt 32pt 36pt 48pt 64pt" },
                
                // BLOCK FORMATS
                { "block_formats", "Paragraph=p; Heading 1=h1; Heading 2=h2; Heading 3=h3; Heading 4=h4; Heading 5=h5; Heading 6=h6; Preformatted=pre" },
                
                // STYLE FORMATS - Custom formatting options
                { "style_formats", new[] {
                    new { title = "Headings", items = new[] {
                        new { title = "Heading 1", format = "h1" },
                        new { title = "Heading 2", format = "h2" },
                        new { title = "Heading 3", format = "h3" },
                        new { title = "Heading 4", format = "h4" }
                    }},
                    new { title = "Inline", items = new[] {
                        new { title = "Bold", format = "bold" },
                        new { title = "Italic", format = "italic" },
                        new { title = "Underline", format = "underline" },
                        new { title = "Strikethrough", format = "strikethrough" },
                        new { title = "Superscript", format = "superscript" },
                        new { title = "Subscript", format = "subscript" },
                        new { title = "Code", format = "code" }
                    }},
                    new { title = "Blocks", items = new[] {
                        new { title = "Paragraph", format = "p" },
                        new { title = "Blockquote", format = "blockquote" },
                        new { title = "Code Block", format = "pre" }
                    }}
                }},

                { "codesample_languages", new[] {
                    new { text = "HTML/XML", value = "markup" },
                    new { text = "JavaScript", value = "javascript" },
                    new { text = "TypeScript", value = "typescript" },
                    new { text = "CSS", value = "css" },
                    new { text = "C#", value = "csharp" },
                    new { text = "Python", value = "python" },
                    new { text = "Java", value = "java" },
                    new { text = "C++", value = "cpp" },
                    new { text = "PHP", value = "php" },
                    new { text = "Ruby", value = "ruby" },
                    new { text = "Go", value = "go" },
                    new { text = "Rust", value = "rust" },
                    new { text = "SQL", value = "sql" },
                    new { text = "JSON", value = "json" },
                    new { text = "Bash/Shell", value = "bash" }
                }},

                { "codesample_global_prismjs", true },
                
                // IMAGE UPLOAD CONFIGURATION
                { "automatic_uploads", false },
                { "images_upload_url", "" },
                { "file_picker_types", "image" },
                { "images_reuse_filename", true },
                
                // IMAGE SETTINGS
                { "image_advtab", true },
                { "image_caption", true },
                { "image_description", true },
                { "image_title", true },
                
                // LINK SETTINGS
                { "link_title", true },
                { "link_target_list", new[] {
                    new { text = "Same window", value = "" },
                    new { text = "New window", value = "_blank" }
                }},
                { "default_link_target", "_blank" },
                { "link_assume_external_targets", true },
                
                // PASTE SETTINGS - Allow all styles and formatting
                { "paste_data_images", true },
                { "paste_as_text", false },
                { "paste_merge_formats", true },
                { "paste_retain_style_properties", "all" },
                { "paste_remove_styles_if_webkit", false },
                { "smart_paste", true },
                
                // CONTENT CLEANUP - Minimal cleanup, preserve all formatting
                { "verify_html", false },
                { "cleanup", false },
                { "forced_root_block", "p" },
                { "remove_trailing_brs", true },
                { "element_format", "html" },
                
                // Allow all elements, attributes, and styles
                { "valid_elements", "*[*]" },
                { "valid_children", "+body[style]" },
                { "extended_valid_elements", "*[*]" },
                
                // Keep ALL inline styles
                { "keep_styles", true },
                { "remove_linebreaks", false },
                
                // NEWLINE HANDLING - TinyMCE 8.0 compatible
                { "force_br_newlines", false },
                { "convert_newlines_to_brs", false },
                
                // EDITOR BEHAVIOR
                { "browser_spellcheck", true },
                { "contextmenu", "link image table" },
                { "object_resizing", true },
                
                // COLORS
                { "color_cols", 8 },
                { "custom_colors", true },
                { "color_map", new[] {
                    "000000", "Black",
                    "FFFFFF", "White",
                    "DC2626", "Red",
                    "EF4444", "Light Red",
                    "EA580C", "Orange",
                    "FB923C", "Light Orange",
                    "CA8A04", "Yellow",
                    "FACC15", "Light Yellow",
                    "16A34A", "Green",
                    "4ADE80", "Light Green",
                    "0891B2", "Cyan",
                    "22D3EE", "Light Cyan",
                    "2563EB", "Blue",
                    "60A5FA", "Light Blue",
                    "6366F1", "Indigo",
                    "A78BFA", "Light Indigo",
                    "9333EA", "Purple",
                    "C084FC", "Light Purple",
                    "DB2777", "Pink",
                    "F472B6", "Light Pink",
                    "64748B", "Gray",
                    "94A3B8", "Light Gray"
                }},

                { "content_style", ContentStyle }
            };
        }

        private const string ContentStyle = @"
            @import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap');

            body {
                font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
                font-size: 16px;
                line-height: 1.8;
                color: #1f2937;
                padding: 20px;
                background: #ffffff;
            }

            .mce-content-body[data-mce-placeholder]:not(.mce-visualblocks)::before {
                content: 'Start writing your content here...' !important;
                display: block !important;
                color: #9ca3af !important;
                font-size: 16px !important;
                font-style: normal !important;
                pointer-events: none !important;
            }

            .mce-content-body[data-mce-placeholder]:not(.mce-visualblocks)::after {
                content: '(you can drag and drop images)' !important;
                display: block !important;
                color: #9ca3af !important;
                font-size: 12px !important;
                font-style: italic !important;
                margin-top: 0.25em !important;
                pointer-events: none !important;
            }

            h1, h2, h3, h4, h5, h6 {
                font-weight: 700;
                line-height: 1.3;
                margin-top: 1.5em;
                margin-bottom: 0.5em;
                color: #111827;
            }

            h1 { font-size: 2.25em; }
            h2 { font-size: 1.875em; }
            h3 { font-size: 1.5em; }
            h4 { font-size: 1.25em; }

            p { 
                margin-bottom: 1.25em;
                margin-top: 0;
            }

            strong, b {
                font-weight: 600;
                color: #111827;
            }

            em, i { font-style: italic; }
            u { text-decoration: underline; }
            
            del, s, strike {
                text-decoration: line-through;
            }
            
            sup { vertical-align: super; font-size: 0.75em; }
            sub { vertical-align: sub; font-size: 0.75em; }

            a {
                color: #6366f1;
                text-decoration: underline;
            }
            
            a:hover {
                color: #4f46e5;
            }

            code {
                background: #f3f4f6;
                color: #dc2626;
                padding: 3px 8px;
                border-radius: 6px;
                font-family: 'SF Mono', 'Monaco', 'Menlo', 'Consolas', monospace;
                font-size: 0.9em;
                font-weight: 500;
                border: 1px solid #e5e7eb;
            }

            pre {
                background: #1e293b !important;
                padding: 20px !important;
                margin: 1.5em 0 !important;
                overflow-x: auto !important;
                font-family: 'SF Mono', 'Monaco', 'Menlo', 'Consolas', monospace !important;
                font-size: 14px !important;
                line-height: 1.7 !important;
                border-radius: 8px !important;
            }

            pre code {
                background: none !important;
                padding: 0 !important;
                color: #e5e7eb !important;
                border: none !important;
                display: block !important;
            }

            ul, ol {
                padding-left: 1.5em;
                margin-bottom: 1.25em;
                margin-top: 0;
            }

            li { margin-bottom: 0.5em; }

            table {
                width: 100%;
                border-collapse: collapse;
                margin: 1.5em 0;
                border: 1px solid #e5e7eb;
                border-radius: 8px;
                overflow: hidden;
            }

            th {
                background: #f9fafb;
                padding: 12px;
                text-align: left;
                font-weight: 600;
                border-bottom: 2px solid #e5e7eb;
            }

            td {
                padding: 12px;
                border-bottom: 1px solid #e5e7eb;
            }

            blockquote {
                border-left: 4px solid #6366f1;
                padding-left: 1.5em;
                margin: 1.5em 0;
                color: #4b5563;
                font-style: italic;
                padding: 1em 1.5em;
                background: #f9fafb;
                border-radius: 0 8px 8px 0;
            }

            img {
                max-width: 100%;
                height: auto;
                border-radius: 8px;
                margin: 1.5em 0;
            }
            
            hr {
                border: none;
                border-top: 2px solid #e5e7eb;
                margin: 2em 0;
            }
            
            /* Remove excessive spacing */
            p + p { margin-top: 0; }
            * { max-height: none !important; }
        ";
    }
}