# Base64.NET

## Overview
Base64 encoding and decoding tool.

## Usage
Encode text to base64 string with specifying charset.
```
base64.exe encode -t utf-8 "foo"
```

Encode file to base64 string.
```
base64.exe encode -f "path/to/file"
```

Decode base64 string to text with specifying charset.
```
base64.exe decode -t utf-8 "QmFzZTY0Lk5FVA=="
```

Decode base64 to file.
```
base64.exe decode -f "path/to/src" "path/to/dst"
```
