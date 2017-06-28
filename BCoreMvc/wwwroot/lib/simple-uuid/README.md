Simple UUID
===========

Simply generates a UUID in both Node.js and the browser.  This code was taken from Jeff Ward (jcward.com) and http://stackoverflow.com/questions/105034/how-to-create-a-guid-uuid-in-javascript/21963136#21963136.  I simply wanted to turn it into a bower package and an npm package for simplicity.

## Browser

**Install via Bower**

```
bower install simple-uuid --save
```

**In your HTML**

```html
    <script src="bower_components/simple-uuid/uuid.js"></script>
```

**In your code**
```javascript
var uuid = UUID.generate();
```

## Node.js

**NPM**

```
npm install simple-uuid --save
```

**In your code**
```javascript
var UUID = require('simple-uuid');
var uuid = UUID.generate();
```

