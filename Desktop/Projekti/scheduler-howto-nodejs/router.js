function callMethod (method) {
    return async (req, res) => {
        let result;
 
        try {
            result = await method(req, res);
        } catch (e) {
            result =  {
                action: "error",
                message: e.message
            }
            console.log(e);
        }
 
        res.send(result);
    }
};
 
module.exports = {
    setRoutes (app, prefix, storage) {
        app.get(`${prefix}`, callMethod((req,res) => {
            return storage.getAll(req.query,res);
        }));
 
        app.post(`${prefix}`, callMethod((req) => {
            return storage.insert(req.body);
        }));
 
        app.put(`${prefix}/:id`, callMethod((req) => {
            return storage.update(req.params.id, req.body);
        }));
 
        app.delete(`${prefix}/:id`, callMethod((req) => {
            return storage.delete(req.params.id);
        }));
    }
};