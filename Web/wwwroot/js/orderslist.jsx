class FilterOption extends React.Component {
    constructor(props) {
        super(props);

        this.state = { options: [] };
        this.onSelectChange = this.onSelectChange.bind(this)
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "/order/orderstatus", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ options: data });
        }.bind(this);
        xhr.send();
    }

    componentDidMount() {
        this.loadData();
    }

    onSelectChange(ev) {
        this.props.onChange(ev.target.value)
    }

    render() {
        return (
            <div>
                <span>Фильтрация по статусу:   </span>
                <select onChange={this.onSelectChange}>
                    <option>Все</option>
                    {
                        this.state.options.map((option) =>
                            <option key={option.id} value={option.id}>{option.statusName}</option>
                        )
                    }
                </select>
            </div>
        )
    }


}

class Order extends React.Component {
    constructor(props) {
        super(props);

        this.onStatusChange = this.onStatusChange.bind(this);
    }

    onStatusChange() {
        this.props.onStatusChange(this.props.order.id, 1)
    }

    isUserAdmin() {
        var isAdmin = false;
        var xhr = new XMLHttpRequest();
        xhr.open("get", "/iAdmin", false);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            isAdmin = data;
        }.bind(this);
        xhr.send();
        return isAdmin;
    }

    render() {
        let isAdmin = this.isUserAdmin()
        return (
            <div className="h-50 w-75 m-3 text-light rounded-5 ps-3 pe-3 p-2 d-flex justify-content-between align-items-center bg-secondary">
                <div>
                    <span>id: {this.props.order.id}  </span>
                    <span>Статус {this.props.order.orderType === 1 ? 'Выполнен' : 'Активный'}</span>
                </div>
                <div>
                    {
                        this.props.order.orderType === 0 && isAdmin
                            ? <button className="btn btn-danger ms-3" onClick={this.onStatusChange}>Выполнить задачу</button>
                            : ""
                    }                 
                    <a className="btn btn-success ms-3" href={"order/" + this.props.order.id}>Детали</a>
                </div>
            </div>
        )
    }
}

class OrderList extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            filter: "Все",
            orders: []
        };
        this.onFilterChange = this.onFilterChange.bind(this);
        this.onChange = this.onChange.bind(this);
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", `/order/all?ListType=${this.state.filter}`, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ orders: data });
        }.bind(this);
        xhr.send();
    }

    componentDidMount() {
        this.loadData();
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.filter !== this.state.filter)
            this.loadData();
    }

    onFilterChange(filterId) {
        this.setState({ filter: filterId });
    }

    onChange(orderId, statusid) {
        var xhr = new XMLHttpRequest();
        xhr.open("post", `/order/changestatus/${orderId}?newOrderStatus=${statusid}`, true);
        xhr.onload = function () {
            this.loadData();
        }.bind(this);
        xhr.send();
    }


    render() {
        return (
            <div className="mt-4">
                < FilterOption onChange={this.onFilterChange} />
                <hr />
                <div className="mt-4 d-flex-column">
                    {
                        this.state.orders.map((order) =>
                            < Order key={order.id} order={order} onStatusChange={this.onChange} />
                        )
                    }
                </div>
            </div>);
    }
}

ReactDOM.render(
    < OrderList />,
    document.getElementById("ordersection")
);