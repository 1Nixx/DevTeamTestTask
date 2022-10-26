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
        console.log(ev)
        this.props.onChange(ev.target.value)
    }

    render() {
        return (
            <div>
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
        this.props.onStatusChange(this.props.id, 1)
    }

    render() {
        console.log("323 - " + this.props.status);
        return (
            <div className={"h-50 w-75 m-3 text-light rounded-5 ps-3 p-2" + (this.props.status == 1 ? 'bg-secondary' : 'bg-primary')}>
                <span>id: {this.props.id}  </span>
                <span>Статус {this.props.status === 0 ? 'Выполнен' : 'Активный'}</span>
                <button className="btn btn-danger ms-3" onClick={this.onStatusChange}>Выполнить задачу</button>
                <a className="btn btn-success ms-3" href={"order/" + this.props.id}>Детали</a>
            </div>
        )
    }
}


class ProductList extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            filter: 0,
            products: []
        };
        this.onFilterChange = this.onFilterChange.bind(this);
        this.onChange = this.onChange.bind(this);
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        console.log("ffff - " + this.state.filter);
        xhr.open("get", `/product/all?ShopId=${this.state.filter}`, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            console.log(data)
            this.setState({ products: data });
        }.bind(this);
        xhr.send();
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.filter !== this.state.filter)
            this.loadData();
    }

    onFilterChange(filterId) {
        console.log(filterId)
        this.setState({ filter: filterId });
    }

    onChange(orderId, statusid) {
        var xhr = new XMLHttpRequest();
        console.log("ffff - " + this.state.filter);
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
                            < Order key={order.id} id={order.id} status={order.ordertype} onStatusChange={this.onChange} />
                        )
                    }
                </div>
            </div>);
    }
}
ReactDOM.render(
    < ProductList />,
    document.getElementById("productsection")
);